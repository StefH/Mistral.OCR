﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MistralOCR;
using MistralOCR.Models;

namespace Mistral.OCR.ConsoleApp;

internal class Worker(IMistralOCR client, ILogger<Worker> logger)
{
    private readonly ILogger<Worker> _logger = logger;

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var folder = Path.Combine(userProfile, "OneDrive", "Training", "Generative AI Foundations", "Questions");

            var sourceImagePaths = Directory.GetFiles(folder, "*.png");

            await using var writer = new StreamWriter(Path.Combine(folder, "mistral.md"));

            foreach (var sourceImagePath in sourceImagePaths)
            {
                _logger.LogInformation("Processing image {Image}", sourceImagePath);
                var image = await File.ReadAllBytesAsync(sourceImagePath, cancellationToken);

                var request = new OCRRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    Document = ImageURLChunk.FromBytes(image)
                };
                var response = await client.ProcessAsync(request, cancellationToken);
                var ocr = response.GetContent();

                await writer.WriteLineAsync("### QUESTION:\r\n\r\n" + ocr.Pages[0].Markdown);
                await writer.WriteLineAsync();
                await writer.WriteLineAsync("---");
                await writer.FlushAsync(cancellationToken);
                
                await Task.Delay(5000, cancellationToken);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}