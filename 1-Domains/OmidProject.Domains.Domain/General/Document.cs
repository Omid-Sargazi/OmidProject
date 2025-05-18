using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;
using OmidProject.Frameworks.Contracts.Common.Enums;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Domains.Domain.General;

public class Document : Entity<Guid>
{
    public Document()
    {
    }

    public Document(string name, DocumentType documentType)
    {
        var dateTime = DateTime.Now;
        Year = dateTime.Year.ToString();
        Month = dateTime.Month.ToString();
        Category = documentType.GetDescription();
        Name = name;
    }

    public string Name { get; protected set; }
    public string Year { get; protected set; }
    public string Month { get; protected set; }
    public string Category { get; protected set; }
    public DocumentType DocumentType { get; protected set; }

    public List<AdvertisementImage> AdvertisementImages { get; set; }

    public string GetFilePath()
    {
        return $"\\{Year}\\{Month}\\{Name}";
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetYear(string year)
    {
        Year = year;
    }

    public void SetMonth(string month)
    {
        Month = month;
    }

    public void SetDocumentType(DocumentType documentType)
    {
        DocumentType = documentType;
    }
}