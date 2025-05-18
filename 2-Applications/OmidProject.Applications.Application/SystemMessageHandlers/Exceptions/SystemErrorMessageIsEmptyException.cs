using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.SystemMessageHandlers.Exceptions;

public sealed class SystemErrorMessageIsEmptyException : BusinessException
{
    public SystemErrorMessageIsEmptyException()
        : base(ExceptionCodes.SystemMessage.SystemErrorMessageIsEmpty)
    {
    }

    //public string? Message => "نمیتوان اروری را با پیام خالی ثبت کرد !";
    public string Message => "Can not create system error when messages is empty!";
}