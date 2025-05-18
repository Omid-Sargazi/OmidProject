using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;

public class EditRoleAccessibleFormCommand : Command
{
    public int Id { get; set; }
    public Guid RoleId { get; set; }
    public int AccessibleFormId { get; set; }
}

public class EditRoleAccessibleFormCommandResponse : CommandResponse
{
}