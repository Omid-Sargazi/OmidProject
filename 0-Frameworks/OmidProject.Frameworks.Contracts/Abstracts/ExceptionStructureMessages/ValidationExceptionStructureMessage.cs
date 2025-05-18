using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

public class ValidationExceptionStructureMessage
{
    public ContentLanguage MessageLanguage { get; set; }
    public string[] Message { get; set; }
}