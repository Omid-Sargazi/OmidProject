using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Applications.Contracts.SystemMessageContracts.Queries.DTOs;

public class SystemErrorDto
{
    public SystemErrorDto()
    {
        List = new List<SystemErrorMessageDto>();
    }

    public int Code { get; set; }

    public List<SystemErrorMessageDto> List { get; set; }
}

public class SystemErrorMessageDto
{
    public ContentLanguage MessageLanguage { get; set; }
    public string Prefix { get; set; }
    public string Message { get; set; }
}