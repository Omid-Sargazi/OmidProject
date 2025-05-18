using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.UserManagerContracts.Commands;

public class UpdateUserCommand : Command
{
    public string UserName { get; set; }

    public AspUserNetDto List { get; set; }
}

public class UpdateUserCommandResponse : CommandResponse
{
    public string Message { get; set; }
    public string Prefix { get; set; }
    public int Code { get; set; }
}