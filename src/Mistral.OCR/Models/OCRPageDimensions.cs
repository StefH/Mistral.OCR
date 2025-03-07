using Newtonsoft.Json;

namespace MistralOCR.Models;

/// <summary>
/// Represents the dimensions of a PDF page's screenshot image.
/// </summary>
public class OCRPageDimensions
{
    /// <summary>
    /// Dots per inch of the page-image.
    /// </summary>
    [JsonProperty("dpi")]
    public int Dpi { get; set; }

    /// <summary>
    /// Height of the image in pixels.
    /// </summary>
    [JsonProperty("height")]
    public int Height { get; set; }

    /// <summary>
    /// Width of the image in pixels.
    /// </summary>
    [JsonProperty("width")]
    public int Width { get; set; }
}