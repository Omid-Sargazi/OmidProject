using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers.Exception;
using OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers;

public class
    EditFrontPageFormCommandHandler : CommandHandler<EditFrontPageFormCommand, EditFrontPageFormCommandResponse>
{
    private readonly IFrontPageFormRepository _frontPageFormRepository;

    public EditFrontPageFormCommandHandler(IFrontPageFormRepository frontPageFormRepository)
    {
        _frontPageFormRepository = frontPageFormRepository;
    }

    public override async Task<EditFrontPageFormCommandResponse> Executor(EditFrontPageFormCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        var frontPageForm = await _frontPageFormRepository.GetById(command.Id);

        if (frontPageForm == null) throw new FrontPageFormNotFoundException();

        if (await _frontPageFormRepository.IsExistAnotherByRouteAsync(command.Id, command.Route))
            throw new RoutIsAlreadyExistException();

        frontPageForm.Update(command.Title, command.Route);

        _frontPageFormRepository.Update(frontPageForm);

        return new EditFrontPageFormCommandResponse();
    }

    private void Guard(EditFrontPageFormCommand command)
    {
        if (command.Id.IsInvalidId()) throw new IdIsInvalidIdException();
        if (command.Route.IsNullOrEmpty()) throw new RouteIsNullOrEmptyException();
        if (command.Title.IsNullOrEmpty()) throw new TitleIsNullOrEmptyException();
    }
}