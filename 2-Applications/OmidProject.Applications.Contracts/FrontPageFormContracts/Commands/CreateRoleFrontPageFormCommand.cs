using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;

public class CreateRoleFrontPageFormCommand : Command
{
    public Guid RoleId { get; set; }
    public int FrontPageFormId { get; set; }
}

public class CreateRoleFrontPageFormCommandResponse : CommandResponse
{
}