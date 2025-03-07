using Newtonsoft.Json;

namespace MistralOCR.Models;

/// <summary>
/// Represents an OCR response with pages, model, and usage information.
/// </summary>
public class OCRResponse
{
    /// <summary>
    /// List of OCR info for pages.
    /// </summary>
    [JsonProperty("pages")]
    public List<OCRPageObject> Pages { get; set; }

    /// <summary>
    /// The model used to generate the OCR.
    /// </summary>
    [JsonProperty("model")]
    public string Model { get; set; }

    /// <summary>
    /// Usage info for the OCR request.
    /// </summary>
    [JsonProperty("usage_info")]
    public OCRUsageInfo UsageInfo { get; set; }
}