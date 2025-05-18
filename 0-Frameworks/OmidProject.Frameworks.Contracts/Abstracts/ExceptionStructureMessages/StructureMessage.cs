using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

public sealed class StructureMessage
{
    public ContentLanguage MessageLanguage { get; set; }
    public string[] Message { get; set; }
}