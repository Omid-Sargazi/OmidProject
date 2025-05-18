using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.FileContracts.Commands;
using OmidProject.Frameworks.Utilities.Extensions;
using OmidProject.IntegrationTests.Services.Interfaces;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace OmidProject.IntegrationTests.Services.Implementations;

public class FileControllerService : BaseTestService, IFileControllerService
{
    /// <summary>
    /// Asynchronously uploads a fake PDF document to a specified API endpoint.
    /// </summary>
    /// <param name="httpClient">The HttpClient used to send the request.</param>
    /// <returns>A task representing the asynchronous operation, with the upload response as the result.</returns>
    public async Task<UploadBase64DocCommandResponse> UploadBase64DocAsync(HttpClient httpClient)
    {
        using var content = new MultipartFormDataContent();
        var fakePdf = GenerateFakePdf(); // Generate the fake PDF

        var pdfContent = new ByteArrayContent(fakePdf);
        pdfContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

        // Add the fake PDF bytes to the content
        content.Add(pdfContent, "files", "test.pdf");

        var response = await SendPostRequestAsync("/api/v1.0/File/upload-base64-doc",
            content, httpClient);
        return await response.DeserializeResponseAsync<UploadBase64DocCommandResponse>();
    }

    /// <summary>
    /// Generates a fake PDF document as a byte array.
    /// </summary>
    /// <returns>A byte array representing the fake PDF document.</returns>
    private static byte[] GenerateFakePdf()
    {
        using var memoryStream = new MemoryStream();
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Arial", 20);

        gfx.DrawString("این یک PDF تستی است.", font, XBrushes.Black, new XPoint(50, 100));

        document.Save(memoryStream);
        return memoryStream.ToArray();
    }
}