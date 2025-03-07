using MistralOCR.Models;
using RestEase;

namespace MistralOCR;

[Header("User-Agent", "stefh/MistralOCR")]
[Header("Authorization", "Bearer")]
public interface IMistralOCR
{
    [Post]
    [AllowAnyStatusCode]
    Task<Response<OCRResponse>> ProcessAsync([Body] OCRRequest request, CancellationToken cancellationToken = default);
}