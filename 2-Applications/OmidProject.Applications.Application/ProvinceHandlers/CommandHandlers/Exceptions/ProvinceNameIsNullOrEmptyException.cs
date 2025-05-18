using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers.Exceptions;

public sealed class ProvinceNameIsNullOrEmptyException : BusinessException
{
    public ProvinceNameIsNullOrEmptyException()
        : base(ExceptionCodes.ProvinceException.ProvinceNameIsNullOrEmpty)
    {
    }

    public override string Message => "نام استان خالی می باشد!";
}