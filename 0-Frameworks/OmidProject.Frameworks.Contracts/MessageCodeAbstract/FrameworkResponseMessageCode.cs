using OmidProject.Frameworks.Contracts.Abstracts.MessageAbstract;

namespace OmidProject.Frameworks.Contracts.MessageCodeAbstract;

public sealed class FrameworkResponseMessageCode : ResponseMessageCode
{
    public FrameworkResponseMessageCode(CodeAndMessage codeAndMessage) : base("FrameworkException", codeAndMessage.Code,
        codeAndMessage.Message)
    {
    }

    public static implicit operator FrameworkResponseMessageCode(CodeAndMessage codeAndMessage)
    {
        return new FrameworkResponseMessageCode(codeAndMessage);
    }
}