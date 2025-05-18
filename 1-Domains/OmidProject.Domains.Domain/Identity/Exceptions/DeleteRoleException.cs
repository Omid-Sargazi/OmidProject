using OmidProject.Domains.Domain.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Domains.Domain.Identity.Exceptions;

public sealed class DeleteRoleException : BusinessException
{
    public DeleteRoleException(string message)
        : base(ExceptionCodes.Roll.DeleteRollError)
    {
        Message = message;
    }

    public override string Message { get; }
}