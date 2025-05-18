using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.SystemMessageHandlers.Exceptions;

public sealed class SystemErrorCanNotFoundException : BusinessException
{
    public SystemErrorCanNotFoundException()
        : base(ExceptionCodes.SystemMessage.SystemErrorCanNotFound)
    {
    }

    //public string? Message => "ارور ناشناخته ای رخ داد !";
    public string Message => "Can not found system error!";
}