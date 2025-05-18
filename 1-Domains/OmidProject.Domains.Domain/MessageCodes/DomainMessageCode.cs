using OmidProject.Frameworks.Contracts.Abstracts.MessageAbstract;

namespace OmidProject.Domains.Domain.MessageCodes;

public sealed class DomainExceptionMessageCode : ExceptionMessageCode
{
    public DomainExceptionMessageCode(int code) : base("DomainException", code)
    {
    }

    public static implicit operator DomainExceptionMessageCode(int first)
    {
        return new DomainExceptionMessageCode(first);
    }
}