using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Abstracts;

namespace OmidProject.Infrastructures.CommandDb.Repository;

public class DocumentRepository : BaseRepository, IDocumentRepository
{
    public DocumentRepository(OmidProjectCommandDb db) : base(db)
    {
    }

    public void Add(Document document)
    {
        _Db.Documents.Add(document);
        _Db.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var document = GetById(id);
        _Db.Documents.Remove(document);
        _Db.SaveChanges();
    }

    public Document GetById(Guid id)
    {
        var result = _Db.Documents.FirstOrDefault(f => f.Id == id);

        return result;
    }

    public Document GetByName(string name)
    {
        var result = _Db.Documents.FirstOrDefault(f => f.Name == name);

        return result;
    }

    public void Update(Document document)
    {
        _Db.Documents.Update(document);

        _Db.SaveChanges();
    }

    public Document GetByOwnerId(Metadata metadata)
    {
        var result = _Db.Documents
            .Where(x => x.CreatedBy == metadata.UserId)
            .OrderByDescending(x => x.CreatedAt) // مرتب‌سازی بر اساس تاریخ ایجاد به صورت نزولی (آخرین تاریخ اولین مورد)
            .FirstOrDefault();

        return result;
    }

    public bool IsExist(Guid documentId)
    {
        var result = _Db.Documents.Any(x => x.Id == documentId);
        return result;
    }
}