using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers.Exceptions;

public sealed class ProvinceNotExistException : BusinessException
{
    public ProvinceNotExistException()
        : base(ExceptionCodes.ProvinceException.ProvinceNotExist)
    {
    }

    public override string Message => "استان مورد نظر یافت نشد!";
}