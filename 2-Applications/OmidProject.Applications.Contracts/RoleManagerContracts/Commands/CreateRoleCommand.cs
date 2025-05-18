using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.RoleManagerContracts.Commands;

public class CreateRoleCommand : Command
{
    public CreateRoleCommand(string rollName)
    {
        RollName = rollName;
    }

    public string RollName { get; set; }
}

public class CreateRollCommandResponse : CommandResponse
{
    public string Message { get; set; }
    public string Prefix { get; set; }
    public int Code { get; set; }
}