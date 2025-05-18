using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.Exception;

public sealed class RouteIsNullOrEmptyException : BusinessException
{
    public RouteIsNullOrEmptyException()
        : base(ExceptionCodes.FormsAndRolesException.RouteIsNullOrEmpty)
    {
    }

    public override string Message => "آدرس یافت نشد!";
}