namespace OmidProject.Frameworks.Contracts.Abstracts.MessageAbstract;

public abstract class ResponseMessageCode : MessageCode
{
    protected ResponseMessageCode(string prefix, int code, string message) : base(prefix, code)
    {
        Message = message;
    }

    public string Message { get; protected set; }
}