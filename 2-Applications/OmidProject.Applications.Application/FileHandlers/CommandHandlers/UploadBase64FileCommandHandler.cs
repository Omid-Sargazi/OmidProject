using OmidProject.Applications.Contracts.FileContracts.Commands;
using OmidProject.Applications.Contracts.FileContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Repository;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;
using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.Options;

namespace OmidProject.Applications.Application.FileHandlers.CommandHandlers;

public class UploadBase64FileCommandHandler
    : CommandHandler<UploadBase64DocCommand, UploadBase64DocCommandResponse>
{
    private readonly GeneralSettings _generalSettings;
    private readonly IGenericRepository<Document, Guid> _genericRepository;

    public UploadBase64FileCommandHandler(
        IOptions<GeneralSettings> options, IGenericRepository<Document, Guid> genericRepository)
    {
        _genericRepository = genericRepository;
        _generalSettings = options.Value;
    }

    public override async Task<UploadBase64DocCommandResponse> Executor(UploadBase64DocCommand command, CancellationToken cancellationToken)
    {
        if (command.DocumentType.IsUndefined())
            throw new ApplicationException("نوع فایل معتبر نمیباشد!");

        //var lastFileUpload = _documentRepository.GetByOwnerId(command.Metadata);

        //if (lastFileUpload != null)
        //    if (lastFileUpload.CreatedAt.AddMinutes(5) >= DateTime.Now)
        //        throw new AddDocumentFileLimitException();

        var result = new UploadBase64DocCommandResponse();
        result.List = new List<UploadBase64DocCommandResponseDto>();

        foreach (var commandList in command.List)
        {
            commandList.Content = commandList.Content.CleanBase64();

            if (!commandList.Content.IsValidBase64()) throw new ApplicationException("فایل معتبر نمیباشد!");

            var dateTime = DateTime.Now;
            var path = $"\\{dateTime.Year}\\{dateTime.Month}";

            var fileName = SaveFile(commandList.Content, commandList.Extention, path);

            var document = new Document(fileName, command.DocumentType);

            await _genericRepository.ExecuteInTransactionAsync(async () =>
                {
                    await _genericRepository.AddAsync(document);
                }
            );

            var dto = new UploadBase64DocCommandResponseDto();
            dto.Id = document.Id;
            dto.Name = fileName;
            dto.Content = commandList.Content;
            result.List.Add(dto);
        }

        return result;
    }

    private string SaveFile(string content, string extension, string path)
    {
        var directoryPath = _generalSettings.DoucmentPath + path;

        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        var fileName = $"{Guid.NewGuid()}{extension}";

        var filePath = Path.Combine(directoryPath, fileName);

        var bytes = Convert.FromBase64String(content);

        File.WriteAllBytes(filePath, bytes);

        return fileName;
    }
}