using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers.Exceptions;

public sealed class ProvinceIdIsGreaterThan : BusinessException
{
    public ProvinceIdIsGreaterThan()
        : base(ExceptionCodes.ProvinceException.ProvinceNameIsExist)
    {
    }

    public override string Message => "آیدی استان نمیتواند بزرگتر از 33 باشد.!";
}