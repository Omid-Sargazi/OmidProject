using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.CommandHandlers;

public class
    RemoveRoleAccessibleFormCommandHandler : CommandHandler<RemoveRoleAccessibleFormCommand,
        RemoveRoleAccessibleFormCommandResponse>
{
    private readonly IRoleAccessibleFormRepository _roleAccessibleFormRepository;

    public RemoveRoleAccessibleFormCommandHandler(IRoleAccessibleFormRepository roleAccessibleFormRepository)
    {
        _roleAccessibleFormRepository = roleAccessibleFormRepository;
    }


    public override async Task<RemoveRoleAccessibleFormCommandResponse> Executor(
        RemoveRoleAccessibleFormCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        var roleAccessibleForm = await _roleAccessibleFormRepository.GetWithIncludes(command.Id);

        if (roleAccessibleForm == null) throw new RoleAccessibleFormNotFoundException();

        roleAccessibleForm.SetDelete();
        _roleAccessibleFormRepository.Update(roleAccessibleForm);

        return new RemoveRoleAccessibleFormCommandResponse();
    }

    private void Guard(RemoveRoleAccessibleFormCommand command)
    {
        if (command.Id.IsInvalidId()) throw new IdIsInvalidIdException();
    }
}