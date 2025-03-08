using System.ComponentModel.DataAnnotations;

namespace MistralOCR.Options;

[PublicAPI]
public class MistralOCROptions
{
    /// <summary>
    /// The required BaseAddress.
    /// </summary>
    [Required]
    public Uri BaseAddress { get; set; } = new("https://api.mistral.ai/v1/ocr");

    [Required] 
    public string ApiKey { get; set; } = null!;

    /// <summary>
    /// Optional HttpClient name to use.
    /// </summary>
    public string? HttpClientName { get; set; }

    /// <summary>
    /// This timeout in seconds defines the timeout on the HttpClient which is used to call the BaseAddress.
    /// 
    /// Default value is <c>60</c> seconds.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TimeoutInSeconds { get; set; } = 60;

    /// <summary>
    /// The maximum number of retries.
    ///
    /// Default value is <c>3</c>.
    /// </summary>
    [Range(0, 99)]
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Set the rate limit for your Completion API consumption.
    ///
    /// Default value is <c>1</c>.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int MaximumRequestsPerSecond { get; set; } = 1;

    /// <summary>
    /// In addition to Network failures, TaskCanceledException, HTTP 5XX and HTTP 408. Also retry these <see cref="HttpStatusCode"/>s. [Optional]
    /// </summary>
    public HttpStatusCode[]? HttpStatusCodesToRetry { get; set; }
}