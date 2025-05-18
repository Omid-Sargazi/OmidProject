using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.CommandHandlers;

public class
    RemoveAccessibleFormCommandHandler : CommandHandler<RemoveAccessibleFormCommand,
        RemoveAccessibleFormCommandResponse>
{
    private readonly IAccessibleFormRepository _accessibleFormRepository;

    public RemoveAccessibleFormCommandHandler(IAccessibleFormRepository accessibleFormRepository)
    {
        _accessibleFormRepository = accessibleFormRepository;
    }

    public override async Task<RemoveAccessibleFormCommandResponse> Executor(RemoveAccessibleFormCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        var accessibleForm = await _accessibleFormRepository.GetById(command.Id);

        if (accessibleForm == null) throw new AccessibleFormNotFoundException();

        accessibleForm.SetDelete();
        _accessibleFormRepository.Update(accessibleForm);


        return new RemoveAccessibleFormCommandResponse();
    }

    private void Guard(RemoveAccessibleFormCommand command)
    {
        if (command.Id.IsInvalidId()) throw new IdIsInvalidIdException();
    }
}