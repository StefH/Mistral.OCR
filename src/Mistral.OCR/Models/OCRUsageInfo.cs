using Newtonsoft.Json;

namespace Mistral.OCR.Models;

/// <summary>
/// Represents OCR usage information including the number of pages processed and document size in bytes.
/// </summary>
public class OCRUsageInfo
{
    /// <summary>
    /// Number of pages processed.
    /// </summary>
    [JsonProperty("pages_processed")]
    public int PagesProcessed { get; set; }

    /// <summary>
    /// Document size in bytes.
    /// </summary>
    [JsonProperty("doc_size_bytes")]
    public int? DocSizeBytes { get; set; }
}