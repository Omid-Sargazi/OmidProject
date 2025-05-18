using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.UserManagerContracts.Commands;

public class CreateUserCommand : Command
{
    public CreateUserCommand(string userName, string password, string email)
    {
        UserName = userName;
        Password = password;
        Email = email;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public class CreateUserCommandResponse : CommandResponse
{
    public string Message { get; set; }
    public string Prefix { get; set; }
    public int Code { get; set; }
}