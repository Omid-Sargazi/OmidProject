using OmidProject.Domains.Domain.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Domains.Domain.SystemMessages.Exceptions;

public sealed class SystemErrorMessageIsEmptyException : BusinessException
{
    public SystemErrorMessageIsEmptyException()
        : base(ExceptionCodes.InternalSystemMessage.SystemErrorMessageIsEmpty)
    {
    }

    //public string Message => "پیام ارور خالی میباشد";
    public string Message => "System error message is empty!";
}