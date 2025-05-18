using OmidProject.Domains.Domain.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Domains.Domain.Identity.Exceptions;

public sealed class CreateRoleException : BusinessException
{
    public CreateRoleException(string message)
        : base(ExceptionCodes.Roll.CreateRollError)
    {
        Message = message;
    }

    public override string Message { get; }
}