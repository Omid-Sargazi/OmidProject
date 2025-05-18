using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.UserManagerContracts.Commands;

public class AddRoleToUserCommand : Command
{
    public AddRoleToUserCommand(string userName, string userRoleName)
    {
        UserName = userName;
        UserRoleName = userRoleName;
    }

    public string UserName { get; set; }
    public string UserRoleName { get; set; }
}

public class AddRoleToUserCommandResponse : CommandResponse
{
    public string Message { get; set; }
    public string Prefix { get; set; }
    public int Code { get; set; }
}