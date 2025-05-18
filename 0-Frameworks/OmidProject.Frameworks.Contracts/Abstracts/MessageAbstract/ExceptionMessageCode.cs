namespace OmidProject.Frameworks.Contracts.Abstracts.MessageAbstract;

public abstract class ExceptionMessageCode : MessageCode
{
    protected ExceptionMessageCode(string prefix, int code) : base(prefix, code)
    {
    }
}