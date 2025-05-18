using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.RoleManagerContracts.Commands;

public class UpdateRoleCommand : Command
{
    public UpdateRoleCommand(string oldRoleName, string newRoleName)
    {
        OldRoleName = oldRoleName;
        NewRoleName = newRoleName;
    }

    public string OldRoleName { get; set; }
    public string NewRoleName { get; set; }
}

public class UpdateRoleCommandResponse : CommandResponse
{
    public string Message { get; set; }
    public string Prefix { get; set; }
    public int Code { get; set; }
}