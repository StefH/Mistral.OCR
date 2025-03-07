using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mistral.OCR.Models;
using Newtonsoft.Json;

namespace Mistral.OCR.ConsoleApp;

internal class Worker
{
    private readonly IMistralOCR _client;
    private readonly ILogger<Worker> _logger;

    public Worker(IMistralOCR client, ILogger<Worker> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var folder = Path.Combine(userProfile, "OneDrive", "Training", "Generative AI Foundations", "Questions");
            var image = await File.ReadAllBytesAsync(Path.Combine(folder, "1999.png"), cancellationToken);

            var request = new OCRRequest
            {
                Id = Guid.NewGuid().ToString(),
                Document = ImageURLChunk.FromBytes(image),
            };
            var response = await _client.ProcessAsync(request, cancellationToken);
            var ocr = response.GetContent();

            int x = 9;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}