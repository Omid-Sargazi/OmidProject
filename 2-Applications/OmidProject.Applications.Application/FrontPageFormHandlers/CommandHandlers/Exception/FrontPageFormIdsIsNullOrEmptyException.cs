using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers.Exception;

public sealed class FrontPageFormIdsIsNullOrEmptyException : BusinessException
{
    public FrontPageFormIdsIsNullOrEmptyException()
        : base(ExceptionCodes.FormsAndRolesException.FrontPageFormIdsIsNullOrEmpty)
    {
    }

    public override string Message => "فرم دسترسی خالی میباشد!";
}