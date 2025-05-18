using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.Exception;

public sealed class AccessibleFormNotFoundException : BusinessException
{
    public AccessibleFormNotFoundException()
        : base(ExceptionCodes.FormsAndRolesException.AccessibleFormNotFound)
    {
    }

    public override string Message => "دسترسی فرم ها یافت نشد!";
}