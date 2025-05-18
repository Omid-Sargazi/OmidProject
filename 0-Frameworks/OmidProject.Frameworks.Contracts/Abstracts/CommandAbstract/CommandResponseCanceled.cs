using OmidProject.Frameworks.Contracts.MessageCodeAbstract;

namespace OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

public class CommandResponseCanceled : CommandResponse
{
    public CommandResponseCanceled() : base(ResponseCodes.OperationCanceled)
    {
    }

    public static CommandResponse CreateCanceled()
    {
        return new CommandResponseCanceled();
    }
}