using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;

public class CreateRoleAccessibleFormCommand : Command
{
    public Guid RoleId { get; set; }
    public int AccessibleFormId { get; set; }
}

public class CreateRoleAccessibleFormCommandResponse : CommandResponse
{
}