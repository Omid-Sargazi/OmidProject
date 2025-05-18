using OmidProject.Domains.Domain.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Domains.Domain.SystemMessages.Exceptions;

public sealed class SystemErrorCodeIsInvalidException : BusinessException
{
    public SystemErrorCodeIsInvalidException()
        : base(ExceptionCodes.InternalSystemMessage.SystemErrorCodeIsInvalid)
    {
    }

    //public  string Message => "کد ارور غیر معتبر میباشد";
    public string Message => "System error code is invalid!";
}