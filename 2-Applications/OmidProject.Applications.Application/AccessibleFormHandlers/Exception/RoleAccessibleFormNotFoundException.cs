using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.Exception;

public sealed class RoleAccessibleFormNotFoundException : BusinessException
{
    public RoleAccessibleFormNotFoundException()
        : base(ExceptionCodes.FormsAndRolesException.RoleAccessibleFormNotFound)
    {
    }

    public override string Message => "دسترسی فرم بر اساس نقش یافت نشد!";
}