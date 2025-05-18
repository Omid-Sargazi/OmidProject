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

public class SetRoleFrontPageFormListCommandHandler : CommandHandler<SetRoleFrontPageFormListCommand,
    SetRoleFrontPageFormListCommandResponse>
{
    private readonly IFrontPageFormRepository _frontPageFormRepository;
    private readonly IRoleFrontPageFormRepository _roleFrontPageFormRepository;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public SetRoleFrontPageFormListCommandHandler(IFrontPageFormRepository frontPageFormRepository,
        IRoleFrontPageFormRepository roleFrontPageFormRepository, RoleManager<ApplicationRole> roleManager)
    {
        _frontPageFormRepository = frontPageFormRepository;
        _roleFrontPageFormRepository = roleFrontPageFormRepository;
        _roleManager = roleManager;
    }

    public override async Task<SetRoleFrontPageFormListCommandResponse> Executor(
        SetRoleFrontPageFormListCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        var role = await _roleManager.FindByIdAsync(command.RoleId.ToString());

        if (role == null)
            throw new RoleNotFoundException();

        var roleFrontPageFormsList = new List<RoleFrontPageForm>();

        foreach (var itemFrontPageFormId in command.FrontPageFormIds)
        {
            if (!_frontPageFormRepository.IsExist(itemFrontPageFormId)) throw new FrontPageFormNotFoundException();

            if (_roleFrontPageFormRepository.Exists(itemFrontPageFormId, command.RoleId))
                continue;

            var roleAccessibleForm = new RoleFrontPageForm(command.RoleId, itemFrontPageFormId);

            roleFrontPageFormsList.Add(roleAccessibleForm);
        }

        await RemoveUnUsedRecords(command);

        _roleFrontPageFormRepository.AddRange(roleFrontPageFormsList);

        return new SetRoleFrontPageFormListCommandResponse();
    }

    private void Guard(SetRoleFrontPageFormListCommand command)
    {
        if (command.FrontPageFormIds.Count <= 0) throw new AccessibleFormIdsIsNullOrEmptyException();
        if (command.RoleId.IsEmpty()) throw new IdIsInvalidIdException();
    }

    private async Task RemoveUnUsedRecords(SetRoleFrontPageFormListCommand command)
    {
        var currentRecords = await _roleFrontPageFormRepository.GetWithRoleIdWithoutInclude(command.RoleId);

        var unUsedRecords = currentRecords.Where(w => !command.FrontPageFormIds.Contains(w.FrontPageFormId)).ToList();

        foreach (var unUsedRecord in unUsedRecords) unUsedRecord.SetDelete();

        _roleFrontPageFormRepository.UpdateRange(unUsedRecords);
    }
}