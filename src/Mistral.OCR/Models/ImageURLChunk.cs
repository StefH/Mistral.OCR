using Newtonsoft.Json;

namespace MistralOCR.Models;

/// <summary>
/// Represents an image URL chunk with a type and image URL.
/// </summary>
public class ImageURLChunk
{

    [JsonProperty("image_url")]
    public string ImageUrl { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; } = "image_url";

    public static ImageURLChunk FromBytes(byte[] bytes, string mimeType = "image/png")
    {
        return new ImageURLChunk
        {
            ImageUrl = $"data:{mimeType};base64,{Convert.ToBase64String(bytes)}"
        };
    }
}