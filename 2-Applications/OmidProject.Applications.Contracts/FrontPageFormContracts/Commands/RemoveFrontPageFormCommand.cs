using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;

public class RemoveFrontPageFormCommand : Command
{
    public int Id { get; set; }
}

public class RemoveFrontPageFormCommandResponse : CommandResponse
{
}