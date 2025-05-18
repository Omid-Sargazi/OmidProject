using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers.Exception;

public sealed class FrontPageFormNotFoundException : BusinessException
{
    public FrontPageFormNotFoundException()
        : base(ExceptionCodes.FormsAndRolesException.FrontPageFormNotFound)
    {
    }

    public override string Message => "دسترسی فرم ها یافت نشد!";
}