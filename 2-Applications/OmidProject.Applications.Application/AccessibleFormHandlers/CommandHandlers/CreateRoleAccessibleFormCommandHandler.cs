using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.CommandHandlers;

public class
    CreateRoleAccessibleFormCommandHandler : CommandHandler<CreateRoleAccessibleFormCommand,
        CreateRoleAccessibleFormCommandResponse>
{
    private readonly IAccessibleFormRepository _accessibleFormRepository;
    private readonly IRoleAccessibleFormRepository _roleAccessibleFormRepository;
    private readonly RoleManager<ApplicationRole> _roleManager;


    public CreateRoleAccessibleFormCommandHandler(RoleManager<ApplicationRole> roleManager,
        IAccessibleFormRepository accessibleFormRepository, IRoleAccessibleFormRepository roleAccessibleFormRepository)
    {
        _roleManager = roleManager;
        _accessibleFormRepository = accessibleFormRepository;
        _roleAccessibleFormRepository = roleAccessibleFormRepository;
    }

    public override async Task<CreateRoleAccessibleFormCommandResponse> Executor(
        CreateRoleAccessibleFormCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        if (_roleManager.FindByIdAsync(command.RoleId.ToString()) == null) throw new RoleNotFoundException();

        if (!_accessibleFormRepository.IsExist(command.AccessibleFormId)) throw new AccessibleFormNotFoundException();

        if (_roleAccessibleFormRepository.Exists(command.AccessibleFormId, command.RoleId))
            throw new RoleAccessibleFormAlreadyExistException();

        var roleAccessibleForm = new RoleAccessibleForm(command.RoleId, command.AccessibleFormId);
        _roleAccessibleFormRepository.Add(roleAccessibleForm);

        var result = new CreateRoleAccessibleFormCommandResponse();

        return result;
    }

    private void Guard(CreateRoleAccessibleFormCommand command)
    {
        if (command.AccessibleFormId.IsInvalidId()) throw new IdIsInvalidIdException();
        if (command.RoleId.IsEmpty()) throw new IdIsInvalidIdException();
    }
}