using OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers.Exceptions;
using OmidProject.Applications.Contracts.ProvinceContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers;

public class CreateProvinceCommandHandler : CommandHandler<CreateProvinceCommand, CreateProvinceCommandResponse>
{
    private readonly IProvinceRepository _provinceRepository;

    public CreateProvinceCommandHandler(IProvinceRepository provinceRepository)
    {
        _provinceRepository = provinceRepository;
    }

    public override async Task<CreateProvinceCommandResponse> Executor(CreateProvinceCommand command,
        CancellationToken cancellationToken)
    {
        await Guard(command);

        var province = new Province(command.Name);
        await _provinceRepository.AddAsync(province);

        var result = new CreateProvinceCommandResponse();
        result.Id = province.Id;
        return result;
    }

    private async Task Guard(CreateProvinceCommand command)
    {
        if (command.Name.IsNullOrEmpty())
            throw new ProvinceNameIsNullOrEmptyException();

        if (await _provinceRepository.IsExistByNameAsync(command.Name))
            throw new ProvinceNameIsExistException();
    }
}