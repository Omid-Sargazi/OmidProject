using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.RoleManagerContracts.Commands;

public class DeleteRoleCommand : Command
{
    public DeleteRoleCommand(string rollName)
    {
        RollName = rollName;
    }

    public string RollName { get; set; }
}

public class DeleteRoleCommandResponse : CommandResponse
{
    public string Message { get; set; }
    public string Prefix { get; set; }
    public int Code { get; set; }
}