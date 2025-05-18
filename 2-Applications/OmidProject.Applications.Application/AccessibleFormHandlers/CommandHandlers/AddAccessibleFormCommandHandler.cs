using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;
using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.Options;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.CommandHandlers;

public class
    AddAccessibleFormCommandHandler(
        IAccessibleFormRepository accessibleFormRepository,
        IOptions<ProjectPathSettings> projectPathSettings)
    : CommandHandler<AddAccessibleFormCommand, AddAccessibleFormCommandResponse>
{
    private readonly ProjectPathSettings _projectPathSettings = projectPathSettings.Value;

    public override async Task<AddAccessibleFormCommandResponse> Executor(AddAccessibleFormCommand command,
        CancellationToken cancellationToken)
    {
        Guard(command);

        var a = _projectPathSettings.ProjectPath;

        if (await accessibleFormRepository.IsExistByRouteAsync(command.Route)) throw new RoutIsAlreadyExistException();

        var result = new AccessibleForm(command.Title, command.Route);
        accessibleFormRepository.Add(result);

        return new AddAccessibleFormCommandResponse();
    }

    private void Guard(AddAccessibleFormCommand command)
    {
        if (command.Route.IsNullOrEmpty()) throw new RouteIsNullOrEmptyException();
        if (command.Title.IsNullOrEmpty()) throw new TitleIsNullOrEmptyException();
    }
}