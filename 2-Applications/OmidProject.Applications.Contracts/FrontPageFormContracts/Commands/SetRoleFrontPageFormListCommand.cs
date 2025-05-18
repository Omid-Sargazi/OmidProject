using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;

public class SetRoleFrontPageFormListCommand : Command
{
    public Guid RoleId { get; set; }
    public List<int> FrontPageFormIds { get; set; }
}

public class SetRoleFrontPageFormListCommandResponse : CommandResponse
{
}