using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;

public class RemoveRoleAccessibleFormCommand : Command
{
    public int Id { get; set; }
}

public class RemoveRoleAccessibleFormCommandResponse : CommandResponse
{
}