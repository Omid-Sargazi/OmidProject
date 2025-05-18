using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers.Exception;
using OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers;

public class
    RemoveRoleFrontPageFormCommandHandler : CommandHandler<RemoveRoleFrontPageFormCommand,
        RemoveRoleFrontPageFormCommandResponse>
{
    private readonly IRoleFrontPageFormRepository _roleFrontPageFormRepository;

    public RemoveRoleFrontPageFormCommandHandler(IRoleFrontPageFormRepository roleFrontPageFormRepository)
    {
        _roleFrontPageFormRepository = roleFrontPageFormRepository;
    }


    public override async Task<RemoveRoleFrontPageFormCommandResponse> Executor(RemoveRoleFrontPageFormCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        var roleFrontPageForm = await _roleFrontPageFormRepository.GetWithIncludes(command.Id);

        if (roleFrontPageForm == null) throw new RoleFrontPageFormNotFoundException();

        roleFrontPageForm.SetDelete();
        _roleFrontPageFormRepository.Update(roleFrontPageForm);

        return new RemoveRoleFrontPageFormCommandResponse();
    }

    private void Guard(RemoveRoleFrontPageFormCommand command)
    {
        if (command.Id.IsInvalidId()) throw new IdIsInvalidIdException();
    }
}