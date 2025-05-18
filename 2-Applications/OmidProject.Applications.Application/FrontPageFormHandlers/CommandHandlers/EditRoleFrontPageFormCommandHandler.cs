using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers.Exception;
using OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers;

public class
    EditRoleFrontPageFormCommandHandler : CommandHandler<EditRoleFrontPageFormCommand,
        EditRoleFrontPageFormCommandResponse>
{
    private readonly IFrontPageFormRepository _frontPageFormRepository;
    private readonly IRoleFrontPageFormRepository _roleFrontPageFormRepository;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public EditRoleFrontPageFormCommandHandler(IFrontPageFormRepository frontPageFormRepository,
        IRoleFrontPageFormRepository roleFrontPageFormRepository, RoleManager<ApplicationRole> roleManager)
    {
        _frontPageFormRepository = frontPageFormRepository;
        _roleFrontPageFormRepository = roleFrontPageFormRepository;
        _roleManager = roleManager;
    }

    public override async Task<EditRoleFrontPageFormCommandResponse> Executor(EditRoleFrontPageFormCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        var roleFrontPageForm = await _roleFrontPageFormRepository.GetWithIncludes(command.Id);

        if (roleFrontPageForm != null) throw new RoleAccessibleFormNotFoundException();

        if (_roleManager.FindByIdAsync(command.RoleId.ToString()) == null) throw new RoleNotFoundException();

        if (!_frontPageFormRepository.IsExist(command.FrontPageFormId)) throw new FrontPageFormNotFoundException();

        if (_roleFrontPageFormRepository.Exists(command.FrontPageFormId, command.RoleId, command.Id))
            throw new RoleFrontPageFormAlreadyExistException();

        roleFrontPageForm.Update(command.RoleId, command.FrontPageFormId);
        _roleFrontPageFormRepository.Update(roleFrontPageForm);

        return new EditRoleFrontPageFormCommandResponse();
    }

    private void Guard(EditRoleFrontPageFormCommand command)
    {
        if (command.Id.IsInvalidId() || command.FrontPageFormId.IsInvalidId()) throw new IdIsInvalidIdException();

        if (command.RoleId.IsEmpty()) throw new IdIsInvalidIdException();
    }
}