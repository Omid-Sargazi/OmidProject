using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers.Exception;

public sealed class RoleFrontPageFormNotFoundException : BusinessException
{
    public RoleFrontPageFormNotFoundException()
        : base(ExceptionCodes.FormsAndRolesException.RoleFrontPageFormNotFound)
    {
    }

    public override string Message => "دسترسی فرم بر اساس نقش یافت نشد!";
}