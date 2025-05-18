using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.Exception;

public sealed class AccessibleFormIdsIsNullOrEmptyException : BusinessException
{
    public AccessibleFormIdsIsNullOrEmptyException()
        : base(ExceptionCodes.FormsAndRolesException.AccessibleFormIdsIsNullOrEmpty)
    {
    }

    public override string Message => "فرم دسترسی خالی میباشد!";
}