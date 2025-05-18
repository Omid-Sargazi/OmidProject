using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;

public class EditRoleFrontPageFormCommand : Command
{
    public int Id { get; set; }
    public Guid RoleId { get; set; }
    public int FrontPageFormId { get; set; }
}

public class EditRoleFrontPageFormCommandResponse : CommandResponse
{
}