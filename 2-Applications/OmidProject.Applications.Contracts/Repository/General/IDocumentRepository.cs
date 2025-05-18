
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Abstracts;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository.General;

public interface IDocumentRepository : IRepository
{
    void Add(Document document);
    void Delete(Guid id);
    Document GetById(Guid id);
    Document GetByName(string name);
    void Update(Document document);
    Document GetByOwnerId(Metadata metadata);
    bool IsExist(Guid documentId);
}