using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers.Exceptions;
using OmidProject.Applications.Contracts.ProvinceContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers;

public class UpdateProvinceCommandHandler : CommandHandler<UpdateProvinceCommand, UpdateProvinceCommandResponse>
{
    private readonly IProvinceRepository _provinceRepository;

    public UpdateProvinceCommandHandler(IProvinceRepository provinceRepository)
    {
        _provinceRepository = provinceRepository;
    }

    public override async Task<UpdateProvinceCommandResponse> Executor(UpdateProvinceCommand command,
        CancellationToken cancellationToken)
    {
        await Guard(command);

        var province = await _provinceRepository.GetByIdAsync(command.Id);

        if (province == null)
            throw new ProvinceNotExistException();

        province.Update(command.Name);

        await _provinceRepository.UpdateAsync(province);

        var result = new UpdateProvinceCommandResponse();
        return result;
    }

    private async Task Guard(UpdateProvinceCommand command)
    {
        if (command.Id.IsInvalidId())
            throw new IdIsInvalidIdException();

        if (command.Name.IsNullOrEmpty())
            throw new ProvinceNameIsNullOrEmptyException();

        if (await _provinceRepository.IsExistByNameAsync(command.Name))
            throw new ProvinceNameIsExistException();
    }
}