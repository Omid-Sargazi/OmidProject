using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers.Exceptions;

public sealed class ProvinceNameIsExistException : BusinessException
{
    public ProvinceNameIsExistException()
        : base(ExceptionCodes.ProvinceException.ProvinceNameIsExist)
    {
    }

    public override string Message => "نام استان تکراری می باشد!";
}