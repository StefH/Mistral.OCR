using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MistralOCR.Options;
using MistralOCR.RetryPolicies;
using Newtonsoft.Json;
using RestEase.HttpClientFactory;
using Stef.Validation;

// ReSharper disable once CheckNamespace
namespace MistralOCR.DependencyInjection;

[PublicAPI]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMistralOCR(this IServiceCollection services, IConfiguration configuration)
    {
        Guard.NotNull(services);
        Guard.NotNull(configuration);

        return services.AddMistralOCR(restEaseClientOptions =>
        {
            configuration.GetSection(nameof(MistralOCROptions)).Bind(restEaseClientOptions);
        });
    }

    public static IServiceCollection AddMistralOCR(this IServiceCollection services, IConfigurationSection section, JsonSerializerSettings? jsonSerializerSettings = null)
    {
        Guard.NotNull(services);
        Guard.NotNull(section);

        return services.AddMistralOCR(section.Bind);
    }

    public static IServiceCollection AddMistralOCR(this IServiceCollection services, Action<MistralOCROptions> configureAction)
    {
        Guard.NotNull(services);
        Guard.NotNull(configureAction);

        var options = new MistralOCROptions();
        configureAction(options);

        return services.AddMistralOCR(options);
    }

    public static IServiceCollection AddMistralOCR(this IServiceCollection services, MistralOCROptions options)
    {
        Guard.NotNull(services);
        Guard.NotNull(options);

        if (string.IsNullOrEmpty(options.HttpClientName))
        {
            options.HttpClientName = "MistralOCR";
        }

        services.AddOptionsWithDataAnnotationValidation(options);

        services
            .AddHttpClient(options.HttpClientName!, httpClient =>
            {
                httpClient.BaseAddress = options.BaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
            })
            .AddPolicyHandler((serviceProvider, _) => HttpClientRetryPolicies.GetPolicy<IMistralOCR>(serviceProvider, options.MaxRetries, options.HttpStatusCodesToRetry))
            .UseWithRestEaseClient(new UseWithRestEaseClientOptions<IMistralOCR>
            {
                InstanceConfigurer = client =>
                {
                    //client.ApiKey = options.ApiKey;
                },
                RequestModifier = (request, _) =>
                {
                    var auth = request.Headers.Authorization;
                    if (auth != null)
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, options.ApiKey);
                    }

                    return Task.CompletedTask;
                }
            });

        return services;
    }
}