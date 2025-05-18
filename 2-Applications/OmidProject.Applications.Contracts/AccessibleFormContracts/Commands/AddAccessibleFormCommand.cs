using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;

public class AddAccessibleFormCommand : Command
{
    public string Title { get; set; }
    public string Route { get; set; }
}

public class AddAccessibleFormCommandResponse : CommandResponse
{
}