using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.Exception;

public sealed class RoleAccessibleFormAlreadyExistException : BusinessException
{
    public RoleAccessibleFormAlreadyExistException()
        : base(ExceptionCodes.FormsAndRolesException.RoleAccessibleFormAlreadyExist)
    {
    }

    public override string Message => "درسترسی برای نقش تکرای است!";
}