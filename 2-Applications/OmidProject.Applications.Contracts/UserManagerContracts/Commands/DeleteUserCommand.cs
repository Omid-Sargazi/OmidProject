using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.UserManagerContracts.Commands;

public class DeleteUserCommand : Command
{
    public DeleteUserCommand(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; }
}

public class DeleteUserCommandResponse : CommandResponse
{
    public string Message { get; set; }
    public string Prefix { get; set; }
    public int Code { get; set; }
}