using OmidProject.Applications.Contracts.FileContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IDocumentService : IService
{
    string GetBase64(Guid documentId);
    GetFileDto GenerateFileDto(Guid documentId);
    string GetBase64WithoutException(Guid documentId);
}