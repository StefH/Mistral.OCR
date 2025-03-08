using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MistralOCR;
using MistralOCR.Models;
using MistralOCR.Services;

namespace Mistral.OCR.ConsoleApp;

internal class Worker(IMistralOCR client, IImageURLHelper imageURLHelper, ILogger<Worker> logger)
{
    public async Task ProcessPdfsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var folder = Path.Combine(userProfile, "OneDrive", "Training", "Generative AI Foundations", "Papers");

            await using var writer = new StreamWriter(Path.Combine(folder, "mistral_pdf.md"));

            foreach (var pdfUrl in new[] { "https://pdfobject.com/pdf/sample.pdf" })
            {
                logger.LogInformation("Processing PDF {Pdf}", pdfUrl);

                var request = new OCRRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    Document = new DocumentURLChunk
                    {
                        DocumentName = pdfUrl.Split('/').Last(),
                        DocumentUrl = pdfUrl
                    }
                };
                var response = await client.ProcessAsync(request, cancellationToken);
                var ocr = response.GetContent();

                foreach (var page in ocr.Pages)
                {
                    await writer.WriteLineAsync(page.Markdown);
                }
                await writer.FlushAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public async Task ProcessImagesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var folder = Path.Combine(userProfile, "OneDrive", "Training", "Generative AI Foundations", "Questions");

            var sourceImagePaths = Directory.GetFiles(folder, "*.png");

            await using var writer = new StreamWriter(Path.Combine(folder, "mistral.md"));

            foreach (var sourceImagePath in sourceImagePaths)
            {
                logger.LogInformation("Processing image {Image}", sourceImagePath);

                var request = new OCRRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    Document = await imageURLHelper.FromFileAsync(sourceImagePath, cancellationToken)
                };
                var response = await client.ProcessAsync(request, cancellationToken);
                var ocr = response.GetContent();

                await writer.WriteLineAsync("### QUESTION:\r\n\r\n" + ocr.Pages[0].Markdown);
                await writer.WriteLineAsync();
                await writer.WriteLineAsync("---");
                await writer.FlushAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}