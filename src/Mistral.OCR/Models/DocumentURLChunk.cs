using Newtonsoft.Json;

namespace MistralOCR.Models;

/// <summary>
/// Represents a document URL chunk with a type, document URL, and an optional document name.
/// </summary>
public class DocumentURLChunk
{
    [JsonProperty("type")]
    public string Type { get; set; } = "document_url";

    [JsonProperty("document_url")]
    public string DocumentUrl { get; set; }

    /// <summary>
    /// The filename of the document.
    /// </summary>
    [JsonProperty("document_name")]
    public string? DocumentName { get; set; }
}