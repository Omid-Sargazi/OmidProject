using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.Exception;

public sealed class RoutIsAlreadyExistException : BusinessException
{
    public RoutIsAlreadyExistException()
        : base(ExceptionCodes.FormsAndRolesException.RoutIsAlreadyExist)
    {
    }

    public override string Message => "آدرس تکراری میباشد!";
}