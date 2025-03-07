using Newtonsoft.Json;

namespace Mistral.OCR.Models;

/// <summary>
/// Represents an OCR request with various properties including model, id, document, pages, and image settings.
/// </summary>
public class OCRRequest
{
    [JsonProperty("model")] 
    public string Model { get; set; } = "mistral-ocr-latest";

    [JsonProperty("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Document to run OCR on.
    /// </summary>
    [JsonProperty("document")]
    public required object Document { get; set; }

    /// <summary>
    /// Specific pages user wants to process in various formats: single number, range, or list of both. Starts from 0.
    /// </summary>
    [JsonProperty("pages")]
    public object? Pages { get; set; }

    /// <summary>
    /// Include image URLs in response.
    /// </summary>
    [JsonProperty("include_image_base64")]
    public bool? IncludeImageBase64 { get; set; }

    /// <summary>
    /// Max images to extract.
    /// </summary>
    [JsonProperty("image_limit")]
    public int? ImageLimit { get; set; }

    /// <summary>
    /// Minimum height and width of image to extract.
    /// </summary>
    [JsonProperty("image_min_size")]
    public int? ImageMinSize { get; set; }
}