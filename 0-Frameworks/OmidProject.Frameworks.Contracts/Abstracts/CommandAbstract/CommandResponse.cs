using OmidProject.Frameworks.Contracts.Abstracts.MessageAbstract;
using Newtonsoft.Json;

namespace OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

public abstract class CommandResponse
{
    protected CommandResponse()
    {
    }

    protected CommandResponse(ResponseMessageCode messageCode)
    {
        Prefix = messageCode.Prefix;
        Code = messageCode.Code;
        Message = messageCode.Message;
    }

    [JsonIgnore] public string Prefix { get; protected set; }

    [JsonIgnore] public int Code { get; protected set; }

    [JsonIgnore] public string Message { get; protected set; }
}