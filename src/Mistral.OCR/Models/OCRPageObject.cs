using Newtonsoft.Json;

namespace MistralOCR.Models;

/// <summary>
/// Represents an OCR page object with index, markdown, images, and dimensions.
/// </summary>
public class OCRPageObject
{
    /// <summary>
    /// The page index in a PDF document starting from 0.
    /// </summary>
    [JsonProperty("index")]
    public int Index { get; set; }

    /// <summary>
    /// The markdown string response of the page.
    /// </summary>
    [JsonProperty("markdown")]
    public string Markdown { get; set; }

    /// <summary>
    /// List of all extracted images in the page.
    /// </summary>
    [JsonProperty("images")]
    public List<OCRImageObject> Images { get; set; }

    /// <summary>
    /// The dimensions of the PDF Page's screenshot image.
    /// </summary>
    [JsonProperty("dimensions")]
    public OCRPageDimensions? Dimensions { get; set; }
}