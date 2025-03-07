## Mistral OCR
Unofficial [RestEase](https://github.com/canton7/RestEase) C# Client for [Mistral OCR](https://api.mistral.ai/v1/ocr).

### Configuration

You will need your ApiKey to use Mistral OCR, you can get one [https://console.mistral.ai/home](https://console.mistral.ai/home).

Register the client using Dependency Injection:

``` csharp
services.AddMistralOCR(o =>
    o.ApiKey = "[YOUR_API_KEY]"
);
```

### Usage
``` csharp
IMistralOCR client = // get from DI
IImageURLHelper imageURLHelper = // get from DI

var sourceImagePath = @"c:\temp\image.png";

var request = new OCRRequest
{
    Id = Guid.NewGuid().ToString(),
    Document = await imageURLHelper.FromFileAsync(sourceImagePath)
};

var response = await client.ProcessAsync(request);
var ocr = response.GetContent();

var markdown = ocr.Pages[0].Markdown;
```

#### Options
``` csharp
public class MistralOCROptions
{
    /// <summary>
    /// The required BaseAddress.
    /// </summary>
    [Required]
    public Uri BaseAddress { get; set; } = new("https://api.mistral.ai/v1/ocr");

    /// <summary>
    /// The required ApiKey.
    /// </summary>
    [Required]
    public string ApiKey { get; set; } = null!;

    /// <summary>
    /// Optional HttpClient name to use.
    /// </summary>
    public string? HttpClientName { get; set; }

    /// <summary>
    /// This timeout in seconds defines the timeout on the HttpClient which is used to call the BaseAddress.
    /// Default value is 60 seconds.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TimeoutInSeconds { get; set; } = 60;

    /// <summary>
    /// The maximum number of retries.
    /// </summary>
    [Range(0, 99)]
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// In addition to Network failures, TaskCanceledException, HTTP 5XX and HTTP 408. Also retry these <see cref="HttpStatusCode"/>s. [Optional]
    /// </summary>
    public HttpStatusCode[]? HttpStatusCodesToRetry { get; set; }
}
```

### References
- https://docs.mistral.ai/api/#tag/ocr