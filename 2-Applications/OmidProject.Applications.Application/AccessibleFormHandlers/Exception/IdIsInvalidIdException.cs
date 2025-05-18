using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.Exception;

public sealed class IdIsInvalidIdException : BusinessException
{
    public IdIsInvalidIdException()
        : base(ExceptionCodes.Public.IdIsInvalid)
    {
    }

    public override string Message => "آیدی نامعتبر است!";
}