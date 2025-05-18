using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers.Exception;
using OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers;

public class
    RemoveFrontPageFormCommandHandler : CommandHandler<RemoveFrontPageFormCommand, RemoveFrontPageFormCommandResponse>
{
    private readonly IFrontPageFormRepository _frontPageFormRepository;

    public RemoveFrontPageFormCommandHandler(IFrontPageFormRepository frontPageFormRepository)
    {
        _frontPageFormRepository = frontPageFormRepository;
    }

    public override async Task<RemoveFrontPageFormCommandResponse> Executor(RemoveFrontPageFormCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        var frontPageForm = await _frontPageFormRepository.GetById(command.Id);

        if (frontPageForm == null) throw new FrontPageFormNotFoundException();

        frontPageForm.SetDelete();
        _frontPageFormRepository.Update(frontPageForm);


        return new RemoveFrontPageFormCommandResponse();
    }

    private void Guard(RemoveFrontPageFormCommand command)
    {
        if (command.Id.IsInvalidId()) throw new IdIsInvalidIdException();
    }
}