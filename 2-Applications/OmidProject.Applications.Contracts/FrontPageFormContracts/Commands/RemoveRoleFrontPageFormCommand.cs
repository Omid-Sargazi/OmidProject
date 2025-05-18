using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;

public class RemoveRoleFrontPageFormCommand : Command
{
    public int Id { get; set; }
}

public class RemoveRoleFrontPageFormCommandResponse : CommandResponse
{
}