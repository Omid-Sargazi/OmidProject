using OmidProject.Domains.Domain.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Domains.Domain.Identity.Exceptions;

public sealed class IdentityUsernameOrPasswordException : BusinessException
{
    public IdentityUsernameOrPasswordException()
        : base(ExceptionCodes.Identity.UsernameOrPasswordIncorrect)
    {
    }

    public override string Message { get; }
}