using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.CommandHandlers;

public class
    EditRoleAccessibleFormCommandHandler : CommandHandler<EditRoleAccessibleFormCommand,
        EditRoleAccessibleFormCommandResponse>
{
    private readonly IAccessibleFormRepository _accessibleFormRepository;
    private readonly IRoleAccessibleFormRepository _roleAccessibleFormRepository;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public EditRoleAccessibleFormCommandHandler(IAccessibleFormRepository accessibleFormRepository,
        IRoleAccessibleFormRepository roleAccessibleFormRepository, RoleManager<ApplicationRole> roleManager)
    {
        _accessibleFormRepository = accessibleFormRepository;
        _roleAccessibleFormRepository = roleAccessibleFormRepository;
        _roleManager = roleManager;
    }

    public override async Task<EditRoleAccessibleFormCommandResponse> Executor(EditRoleAccessibleFormCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        var roleAccessibleForm = await _roleAccessibleFormRepository.GetWithIncludes(command.Id);

        if (roleAccessibleForm != null) throw new RoleAccessibleFormNotFoundException();

        if (_roleManager.FindByIdAsync(command.RoleId.ToString()) == null) throw new RoleNotFoundException();

        if (!_accessibleFormRepository.IsExist(command.AccessibleFormId)) throw new AccessibleFormNotFoundException();

        if (_roleAccessibleFormRepository.Exists(command.AccessibleFormId, command.RoleId, command.Id))
            throw new RoleAccessibleFormAlreadyExistException();

        roleAccessibleForm.Update(command.RoleId, command.AccessibleFormId);
        _roleAccessibleFormRepository.Update(roleAccessibleForm);

        return new EditRoleAccessibleFormCommandResponse();
    }

    private void Guard(EditRoleAccessibleFormCommand command)
    {
        if (command.Id.IsInvalidId() || command.AccessibleFormId.IsInvalidId()) throw new IdIsInvalidIdException();

        if (command.RoleId.IsEmpty()) throw new IdIsInvalidIdException();
    }
}