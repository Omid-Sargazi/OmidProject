using OmidProject.Frameworks.Contracts.Abstracts.MessageAbstract;

namespace OmidProject.Frameworks.Contracts.MessageCodeAbstract;

public sealed class FrameworkExceptionMessageCode : ExceptionMessageCode
{
    public FrameworkExceptionMessageCode(int code) : base("FrameworkException", code)
    {
    }

    public static implicit operator FrameworkExceptionMessageCode(int first)
    {
        return new FrameworkExceptionMessageCode(first);
    }
}