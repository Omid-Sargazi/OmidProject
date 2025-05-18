using OmidProject.Frameworks.Contracts.Abstracts.MessageAbstract;

namespace OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

public abstract class BusinessException : Exception
{
    protected BusinessException(MessageCode exceptionCode)
    {
        Code = exceptionCode.Code;
        Prefix = exceptionCode.Prefix;
    }

    public int Code { get; private set; }
    public string Prefix { get; private set; }
}