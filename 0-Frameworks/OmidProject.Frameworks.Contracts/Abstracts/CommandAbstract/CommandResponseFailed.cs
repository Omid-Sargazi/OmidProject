using OmidProject.Frameworks.Contracts.MessageCodeAbstract;

namespace OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

public class CommandResponseFailed : CommandResponse
{
    public CommandResponseFailed() : base(ResponseCodes.OperationFailed)
    {
    }

    public static CommandResponse CreateFailed()
    {
        return new CommandResponseFailed();
    }
}