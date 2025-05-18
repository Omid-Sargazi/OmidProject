using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.FileContracts.Commands;
using OmidProject.Applications.Contracts.FileContracts.Queries;
using OmidProject.Applications.Contracts.FileContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Common.Enums;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Frameworks.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OmidProject.Host.Controllers.General;

public class FileController(IDistributor distributor) : MainController(distributor)
{
    [Description("آپلود فایل مستند")]
    [HttpPost("upload-base64-doc")]
    [FileUploadOperation.FileContentType]
    public async Task<IActionResult> UploadBase64Doc(List<IFormFile> files, CancellationToken cancellationToken)
    {
        var command = new UploadBase64DocCommand();
        command.List = new List<UploadBase64DocCommandDto>();

        var result = new UploadBase64DocCommandResponse();
        result.List = new List<UploadBase64DocCommandResponseDto>();

        foreach (var file in files.Where(file => file is { Length: > 0 }))
        {
            GuardAgainstFileContentType(file);
            GuardAgainstFileSize(file);

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms, cancellationToken);
            var fileBytes = ms.ToArray();
            var content = Convert.ToBase64String(fileBytes);
            var extension = Path.GetExtension(file.FileName);
            // act on the Base64 data
            var dto = new UploadBase64DocCommandDto();
            dto.Content = content;
            dto.Extention = extension;
            command.List.Add(dto);
        }

        command.DocumentType = DocumentType.File;
        result = await
            Distributor.PushCommand<UploadBase64DocCommand,
                UploadBase64DocCommandResponse>(command, cancellationToken);
        return OkApiResult(result);
    }

    [Description("دریافت فایل مستند")]
    [HttpGet("get-base64-doc")]
    public async Task<IActionResult> GetBase64Doc([FromQuery] GetBase64DocQuery query,
        CancellationToken cancellationToken)
    {
        var result =
            Distributor.PullQuery<GetBase64DocQuery,
                GetBase64DocQueryResponse>(query, cancellationToken);

        return OkApiResult(await result);
    }

    private static void GuardAgainstFileSize(IFormFile file)
    {
        if (file.Length > 5 * 1024 * 1024) throw new NotSupportedException($"File size: {file.Length}");
    }

    private static void GuardAgainstFileContentType(IFormFile file)
    {
        if (file.ContentType != "image/jpeg"
            && file.ContentType != "application/pdf"
            && file.ContentType != "image/tiff"
            && file.ContentType != "image/png")
            throw new NotSupportedException(file.ContentType);
    }
}