using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.UserManagerContracts.Commands;

public class DeleteUserRoleCommand : Command
{
    public DeleteUserRoleCommand(string userName, string roleName)
    {
        UserName = userName;
        RoleName = roleName;
    }

    public string UserName { get; set; }
    public string RoleName { get; set; }
}

public class DeleteUserRoleCommandResponse : CommandResponse
{
    public string Message { get; set; }
    public string Prefix { get; set; }
    public int Code { get; set; }
}