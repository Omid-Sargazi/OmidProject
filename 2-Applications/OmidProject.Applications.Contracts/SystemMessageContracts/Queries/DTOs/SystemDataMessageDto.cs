using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Applications.Contracts.SystemMessageContracts.Queries.DTOs;

public class SystemDataMessageDto
{
    public ContentLanguage MessageLanguage { get; set; }
    public string Prefix { get; set; }
    public string Message { get; set; }
}