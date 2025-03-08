using MistralOCR.Models;

namespace MistralOCR.Services;

public interface IImageURLHelper
{
    Task<ImageURLChunk> FromFileAsync(string sourceImagePath, CancellationToken cancellationToken = default);
}