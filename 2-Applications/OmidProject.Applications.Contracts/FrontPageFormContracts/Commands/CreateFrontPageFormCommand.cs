using OmidProject.Applications.Contracts.CustomAttribute;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;

public class CreateFrontPageFormCommand : Command
{
    public string Title { get; set; }
    public string Route { get; set; }
}

public class CreateFrontPageFormCommandResponse : CommandResponse
{
}