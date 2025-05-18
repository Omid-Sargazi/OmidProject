using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Applications.Contracts.SystemMessageContracts.Commands;

public class DeleteSystemMessageByLanguageCommand : Command
{
    public TypeSystemMessage TypeMessage { get; set; }

    public int Code { get; set; }

    public List<ContentLanguage> ListLanguage { get; set; }
}