using OmidProject.Applications.Contracts.FileContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;
using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.Options;

namespace OmidProject.Applications.Application.Service;

public class DocumentService : IDocumentService
{
    private readonly GeneralSettings _generalSettings;

    public DocumentService(IOptions<GeneralSettings> generalSettings)
    {
        _generalSettings = generalSettings.Value;
    }

    public string GetBase64(Guid documentId)
    {
        if (documentId.IsEmpty()) return null;

        Document document = null;

        if (document == null) throw new ApplicationException("سند مورد نظر یافت نشد!");

        var content = GetFileBase64(document.GetFilePath());

        return content;
    }

    public GetFileDto GenerateFileDto(Guid documentId)
    {
        if (documentId.IsEmpty()) return null;

        Document document = null;

        if (document == null) throw new ApplicationException("سند مورد نظر یافت نشد!");

        var result = new GetFileDto();
        result.Id = document.Id;
        result.FileName = document.Name;
        result.Content = GetFileBase64(document.GetFilePath());

        return result;
    }

    public string GetBase64WithoutException(Guid documentId)
    {
        if (documentId.IsEmpty()) return null;

        Document document = null;

        if (document == null) return "document not found";

        var content = GetFileBase64WithoutException(document.GetFilePath());

        return content;
    }

    public bool ExistDocument(Guid documentId)
    {
        Document document = null;
        return true;
    }

    private string GetFileBase64(string documentName)
    {
        var directoryPath = _generalSettings.DoucmentPath;

        var filePath = Path.Combine(directoryPath, documentName);
        filePath = directoryPath + documentName;

        if (!File.Exists(filePath)) throw new ApplicationException("سند مورد نظر یافت نشد!");

        var bytes = File.ReadAllBytes(filePath);

        var result = Convert.ToBase64String(bytes);

        return result;
    }

    private string GetFileBase64WithoutException(string documentName)
    {
        var directoryPath = _generalSettings.DoucmentPath;

        var filePath = Path.Combine(directoryPath, documentName);
        filePath = directoryPath + documentName;

        if (!File.Exists(filePath)) return "document not found";

        var bytes = File.ReadAllBytes(filePath);

        var result = Convert.ToBase64String(bytes);

        return result;
    }
}