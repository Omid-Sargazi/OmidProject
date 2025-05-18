using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers.Exception;
using OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers;

public class
    CreateRoleFrontPageFormCommandHandler : CommandHandler<CreateRoleFrontPageFormCommand,
        CreateRoleFrontPageFormCommandResponse>
{
    private readonly IFrontPageFormRepository _frontPageFormRepository;
    private readonly IRoleFrontPageFormRepository _roleFrontPageFormRepository;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public CreateRoleFrontPageFormCommandHandler(IFrontPageFormRepository frontPageFormRepository,
        IRoleFrontPageFormRepository roleFrontPageFormRepository, RoleManager<ApplicationRole> roleManager)
    {
        _frontPageFormRepository = frontPageFormRepository;
        _roleFrontPageFormRepository = roleFrontPageFormRepository;
        _roleManager = roleManager;
    }

    public override async Task<CreateRoleFrontPageFormCommandResponse> Executor(CreateRoleFrontPageFormCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        if (_roleManager.FindByIdAsync(command.RoleId.ToString()) == null) throw new RoleNotFoundException();

        if (!_frontPageFormRepository.IsExist(command.FrontPageFormId)) throw new FrontPageFormNotFoundException();

        if (_roleFrontPageFormRepository.Exists(command.FrontPageFormId, command.RoleId))
            throw new RoleFrontPageFormAlreadyExistException();

        var roleFrontPageForm = new RoleFrontPageForm(command.RoleId, command.FrontPageFormId);
        _roleFrontPageFormRepository.Add(roleFrontPageForm);

        var result = new CreateRoleFrontPageFormCommandResponse();

        return result;
    }

    private void Guard(CreateRoleFrontPageFormCommand command)
    {
        if (command.FrontPageFormId.IsInvalidId()) throw new IdIsInvalidIdException();
        if (command.RoleId.IsEmpty()) throw new IdIsInvalidIdException();
    }
}