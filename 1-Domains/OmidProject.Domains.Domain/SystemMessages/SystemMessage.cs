using OmidProject.Domains.Domain.SystemMessages.Exceptions;
using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;
using OmidProject.Frameworks.Contracts.Common.Enums;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Domains.Domain.SystemMessages;

public class SystemMessage : Entity<int>
{
    protected SystemMessage()
    {
    }

    public SystemMessage(int code, TypeSystemMessage typeSystemMessage, List<SystemDataMessage> systemErrorMessages)
    {
        GuardForCode(code);
        GuardForCheckSystemErrorMessage(systemErrorMessages);

        Code = code;

        TypeMessage = typeSystemMessage;

        SystemDataMessages = systemErrorMessages;
    }

    public TypeSystemMessage TypeMessage { get; private set; }

    public int Code { get; private set; }

    public List<SystemDataMessage> SystemDataMessages { get; }

    public void UpdateMessage(List<SystemDataMessage>? systemErrorMessages)
    {
        if (systemErrorMessages.IsNullOrEmpty()) return;

        foreach (var item in systemErrorMessages)
        {
            var found = SystemDataMessages.FirstOrDefault(x => x.MessageLanguage == item.MessageLanguage);

            if (found != null)
                found.Update(item.Prefix, item.Message, item.ModifiedBy.ToString());
            else
                SystemDataMessages.Add(item);
        }
    }

    public void DeleteMessageByLanguage(List<ContentLanguage> messageLanguages)
    {
        if (messageLanguages.IsNullOrEmpty()) return;

        foreach (var item in messageLanguages)
        {
            var found = SystemDataMessages.FirstOrDefault(x => x.MessageLanguage == item);

            if (found != null) SystemDataMessages.Remove(found);
        }
    }

    private static void GuardForCode(int code)
    {
        if (code.IsInvalidId()) throw new SystemErrorCodeIsInvalidException();
    }

    private static void GuardForCheckSystemErrorMessage(List<SystemDataMessage> systemErrorMessages)
    {
        if (systemErrorMessages.IsNullOrEmpty()) throw new SystemErrorMessageIsEmptyException();
    }
}