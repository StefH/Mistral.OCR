using MimeDetective;
using MistralOCR.Models;
using Stef.Validation;

namespace MistralOCR.Services;

internal class ImageURLHelper(IContentInspector contentInspector) : IImageURLHelper
{
    public async Task<ImageURLChunk> FromFileAsync(string sourceImagePath, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(sourceImagePath);

        var imageBytes = await File.ReadAllBytesAsync(sourceImagePath, cancellationToken);
        var definitionMatch = contentInspector.Inspect(imageBytes).FirstOrDefault();
        var mimeType = definitionMatch?.Definition.File.MimeType ?? throw new ArgumentException("Unable to determine MimeType for file.");

        return ImageURLChunk.FromBytes(imageBytes, mimeType);
    }
}