using OmidProject.Frameworks.Contracts.MessageCodeAbstract;

namespace OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

public class CommandResponseSuccessful : CommandResponse
{
    public CommandResponseSuccessful() : base(ResponseCodes.OperationSuccessful)
    {
    }

    public static CommandResponse CreateSuccessful()
    {
        return new CommandResponseSuccessful();
    }
}