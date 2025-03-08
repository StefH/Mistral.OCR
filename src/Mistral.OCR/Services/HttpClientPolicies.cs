using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MistralOCR.Options;
using Polly;
using Polly.Extensions.Http;

namespace MistralOCR.Services;

internal static class HttpClientPolicies
{
    internal static IAsyncPolicy<HttpResponseMessage> GetRateLimitAndRetryPolicies<T>(IServiceProvider serviceProvider, MistralOCROptions options) where T : class
    {
        var logger = serviceProvider.GetRequiredService<ILogger<T>>();

#if !NET8_0_OR_GREATER
        logger.LogWarning("Rate limiting is only supported on .NET 8.0 or greater. Skipping rate limiting policy.");
        return GetRetryPolicy(logger, options);
#else
        var rateLimit = GetRateLimitPolicy(logger, options);
        var retry = GetRetryPolicy(logger, options);

        return Policy.WrapAsync(rateLimit, retry);
#endif
    }

#if NET8_0_OR_GREATER
    private static IAsyncPolicy<HttpResponseMessage> GetRateLimitPolicy<T>(ILogger<T> logger, MistralOCROptions options) where T : class
    {
        var requestsPerMinute = options.MaximumRequestsPerSecond * 60;
        var minute = TimeSpan.FromSeconds(60);

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(httpResponseMessage => httpResponseMessage.StatusCode == HttpStatusCode.TooManyRequests)
            .OrInner<Polly.RateLimit.RateLimitRejectedException>()
            .WaitAndRetryForeverAsync((retryNum, _) =>
            {
                logger.LogWarning("Request failed with '{reason}'. Retrying request. Retry attempt {retryCount}.", HttpStatusCode.TooManyRequests, retryNum);
                return minute / requestsPerMinute;
            })
            .WrapAsync(Policy.RateLimitAsync(requestsPerMinute, minute));
    }
#endif

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy<T>(ILogger<T> logger, MistralOCROptions options) where T : class
    {
        var policyBuilder = HttpPolicyExtensions.HandleTransientHttpError();

        if (options.HttpStatusCodesToRetry is { Length: > 0 })
        {
            policyBuilder = policyBuilder.OrResult(httpResponseMessage => options.HttpStatusCodesToRetry.Contains(httpResponseMessage.StatusCode));
        }

        return policyBuilder
            .OrInner<TaskCanceledException>()
            .WaitAndRetryAsync(options.MaxRetries, retryCount => TimeSpan.FromSeconds(Math.Pow(2, retryCount)), (result, timeSpan, retryCount, _) =>
            {
                var reason = result?.Result?.StatusCode.ToString() ?? result?.Exception.Message;

                logger.LogWarning("Request failed with '{reason}'. Waiting {timeSpan} before next retry. Retry attempt {retryCount}/{totalRetryCount}.", reason, timeSpan, retryCount, options.MaxRetries);
            });
    }
}