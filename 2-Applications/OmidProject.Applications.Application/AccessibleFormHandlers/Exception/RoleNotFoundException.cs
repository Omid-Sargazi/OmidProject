using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.Exception;

public sealed class RoleNotFoundException : BusinessException
{
    public RoleNotFoundException()
        : base(ExceptionCodes.FormsAndRolesException.RoleNotFound)
    {
    }

    public override string Message => "نقش یافت نشد!";
}