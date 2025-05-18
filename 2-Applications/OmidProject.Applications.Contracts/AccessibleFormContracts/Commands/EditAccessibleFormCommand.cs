using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;

public class EditAccessibleFormCommand : Command
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Route { get; set; }
}

public class EditAccessibleFormCommandResponse : CommandResponse
{
}