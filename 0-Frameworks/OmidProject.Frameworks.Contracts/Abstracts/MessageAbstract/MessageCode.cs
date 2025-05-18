namespace OmidProject.Frameworks.Contracts.Abstracts.MessageAbstract;

public abstract class MessageCode
{
    protected MessageCode(string prefix, int code)
    {
        Prefix = prefix;
        Code = code;
    }

    public string Prefix { get; protected set; }
    public int Code { get; protected set; }
}