using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.SystemMessageHandlers.Exceptions;

public sealed class SystemErrorCodeIsDuplicateException : BusinessException
{
    public SystemErrorCodeIsDuplicateException()
        : base(ExceptionCodes.SystemMessage.SystemErrorCodeIsDuplicate)
    {
    }

    //public string? Message => "کد ارور مورد نظر وجود دارد !";
    public string Message => "System error code is duplicate!";
}