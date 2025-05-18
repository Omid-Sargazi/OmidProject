using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;
using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Domains.Domain.SystemMessages;

public class SystemDataMessage : Entity<int>
{
    protected SystemDataMessage(string prefix, string message)
    {
        Prefix = prefix;
        Message = message;
    }

    public SystemDataMessage(ContentLanguage messageLanguage, string prefix, string message)
    {
        MessageLanguage = messageLanguage;
        Prefix = prefix;
        Message = message;
    }

    public int LocalId { get; }

    public ContentLanguage MessageLanguage { get; private set; }
    public string Prefix { get; private set; }
    public string Message { get; private set; }

    public void Update(string prefix, string message, string modifyBy)
    {
        Prefix = prefix;
        Message = message;
        ModifiedAt = DateTime.Now;
        ModifiedBy = Guid.Parse(modifyBy);
    }
}