using OmidProject.Domains.Domain.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Domains.Domain.Identity.Exceptions;

public sealed class IdentitySignOutException : BusinessException
{
    public IdentitySignOutException()
        : base(ExceptionCodes.Identity.ClientHaveNoToken)
    {
    }

    public override string Message { get; }
}