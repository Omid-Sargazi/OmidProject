using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.SmsContracts.Commands;

public class SendSmsCommand : Command
{
    public string Receiver { get; set; }
    public string Message { get; set; }
}

public class SendSmsCommandResponse : CommandResponse
{
}