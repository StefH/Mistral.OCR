using Mistral.OCR.Models;
using RestEase;

namespace Mistral.OCR;

[Header("User-Agent", "stefh/MistralOCR")]
[Header("Authorization", "Bearer")]
public interface IMistralOCR
{
    [Post]
    Task<Response<OCRResponse>> ProcessAsync([Body] OCRRequest request, CancellationToken cancellationToken = default);
}