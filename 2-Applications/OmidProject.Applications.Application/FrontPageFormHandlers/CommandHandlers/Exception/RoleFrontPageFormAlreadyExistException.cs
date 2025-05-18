using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers.Exception;

public sealed class RoleFrontPageFormAlreadyExistException : BusinessException
{
    public RoleFrontPageFormAlreadyExistException()
        : base(ExceptionCodes.FormsAndRolesException.RoleFrontPageFormAlreadyExist)
    {
    }

    public override string Message => "درسترسی برای نقش تکرای است!";
}