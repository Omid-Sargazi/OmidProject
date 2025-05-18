using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers;

public class
    CreateFrontPageFormCommandHandler : CommandHandler<CreateFrontPageFormCommand, CreateFrontPageFormCommandResponse>
{
    private readonly IFrontPageFormRepository _frontPageFormRepository;

    public CreateFrontPageFormCommandHandler(IFrontPageFormRepository frontPageFormRepository)
    {
        _frontPageFormRepository = frontPageFormRepository;
    }

    public override async Task<CreateFrontPageFormCommandResponse> Executor(CreateFrontPageFormCommand command, CancellationToken cancellationToken)
    {
        Guard(command);

        if (await _frontPageFormRepository.IsExistByRouteAsync(command.Route)) throw new RoutIsAlreadyExistException();

        var result = new FrontPageForm(command.Title, command.Route);
        _frontPageFormRepository.Add(result);

        return new CreateFrontPageFormCommandResponse();
    }

    private void Guard(CreateFrontPageFormCommand command)
    {
        if (command.Route.IsNullOrEmpty()) throw new RouteIsNullOrEmptyException();
        if (command.Title.IsNullOrEmpty()) throw new TitleIsNullOrEmptyException();
    }
}