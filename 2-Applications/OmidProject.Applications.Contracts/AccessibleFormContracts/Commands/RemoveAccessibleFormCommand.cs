using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;

public class RemoveAccessibleFormCommand : Command
{
    public int Id { get; set; }
}

public class RemoveAccessibleFormCommandResponse : CommandResponse
{
}