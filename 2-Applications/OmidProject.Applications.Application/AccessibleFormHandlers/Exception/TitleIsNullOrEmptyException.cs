using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.Exception;

public sealed class TitleIsNullOrEmptyException : BusinessException
{
    public TitleIsNullOrEmptyException()
        : base(ExceptionCodes.FormsAndRolesException.TitleIsNullOrEmpty)
    {
    }

    public override string Message => "عنوان یافت نشد!";
}