using Newtonsoft.Json;

namespace Mistral.OCR.Models;

/// <summary>
/// Represents an image URL chunk with a type and image URL.
/// </summary>
public class ImageURLChunk
{

    [JsonProperty("image_url")]
    public string ImageUrl { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; } = "image_url";

    public static ImageURLChunk FromBytes(byte[] bytes)
    {
        return new ImageURLChunk()
        {
            ImageUrl = $"data:image/png;base64,{Convert.ToBase64String(bytes)}"
        };
    }
}