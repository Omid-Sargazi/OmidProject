using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.CommandHandlers;

public class SetRoleAccessibleFormListCommandHandler : CommandHandler<SetRoleAccessibleFormListCommand,
    SetRoleAccessibleFormListCommandResponse>
{
    private readonly IAccessibleFormRepository _accessibleFormRepository;
    private readonly IRoleAccessibleFormRepository _roleAccessibleFormRepository;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public SetRoleAccessibleFormListCommandHandler(IAccessibleFormRepository accessibleFormRepository,
        IRoleAccessibleFormRepository roleAccessibleFormRepository, RoleManager<ApplicationRole> roleManager)
    {
        _accessibleFormRepository = accessibleFormRepository;
        _roleAccessibleFormRepository = roleAccessibleFormRepository;
        _roleManager = roleManager;
    }

    public override async Task<SetRoleAccessibleFormListCommandResponse> Executor(
        SetRoleAccessibleFormListCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        var role = await _roleManager.FindByIdAsync(command.RoleId.ToString());

        if (role == null)
            throw new RoleNotFoundException();

        var roleAccessibleFormList = new List<RoleAccessibleForm>();

        foreach (var itemAccessibleFormId in command.AccessibleFormIds)
        {
            if (!_accessibleFormRepository.IsExist(itemAccessibleFormId)) throw new AccessibleFormNotFoundException();

            if (_roleAccessibleFormRepository.Exists(itemAccessibleFormId, command.RoleId))
                continue;

            var roleAccessibleForm = new RoleAccessibleForm(command.RoleId, itemAccessibleFormId);

            roleAccessibleFormList.Add(roleAccessibleForm);
        }

        await RemoveUnUsedRecords(command);

        _roleAccessibleFormRepository.AddRange(roleAccessibleFormList);

        return new SetRoleAccessibleFormListCommandResponse();
    }

    private void Guard(SetRoleAccessibleFormListCommand command)
    {
        if (command.AccessibleFormIds.Count <= 0) throw new AccessibleFormIdsIsNullOrEmptyException();
        if (command.RoleId.IsEmpty()) throw new IdIsInvalidIdException();
    }

    private async Task RemoveUnUsedRecords(SetRoleAccessibleFormListCommand command)
    {
        var currentRecords = await _roleAccessibleFormRepository.GetWithRoleIdWithoutInclude(command.RoleId);

        var unUsedRecords = currentRecords.Where(w => !command.AccessibleFormIds.Contains(w.AccessibleFormId)).ToList();

        foreach (var unUsedRecord in unUsedRecords) unUsedRecord.SetDelete();

        _roleAccessibleFormRepository.UpdateRange(unUsedRecords);
    }
}