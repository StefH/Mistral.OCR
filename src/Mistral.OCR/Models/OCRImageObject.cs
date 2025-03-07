using Newtonsoft.Json;

namespace MistralOCR.Models;

/// <summary>
/// Represents an OCR image object with coordinates and base64 string of the extracted image.
/// </summary>
public class OCRImageObject
{
    /// <summary>
    /// Image ID for extracted image in a page.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// X coordinate of top-left corner of the extracted image.
    /// </summary>
    [JsonProperty("top_left_x")]
    public int? TopLeftX { get; set; }

    /// <summary>
    /// Y coordinate of top-left corner of the extracted image.
    /// </summary>
    [JsonProperty("top_left_y")]
    public int? TopLeftY { get; set; }

    /// <summary>
    /// X coordinate of bottom-right corner of the extracted image.
    /// </summary>
    [JsonProperty("bottom_right_x")]
    public int? BottomRightX { get; set; }

    /// <summary>
    /// Y coordinate of bottom-right corner of the extracted image.
    /// </summary>
    [JsonProperty("bottom_right_y")]
    public int? BottomRightY { get; set; }

    /// <summary>
    /// Base64 string of the extracted image.
    /// </summary>
    [JsonProperty("image_base64")]
    public string? ImageBase64 { get; set; }
}