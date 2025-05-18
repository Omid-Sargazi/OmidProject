using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Validator;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;
using FluentValidation;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.CommandHandlers;

public class
    EditAccessibleFormCommandHandler : CommandHandler<EditAccessibleFormCommand, EditAccessibleFormCommandResponse>
{
    private readonly IValidator<EditAccessibleFormCommand> _validator;
    private readonly IAccessibleFormRepository _accessibleFormRepository;

    public EditAccessibleFormCommandHandler(IAccessibleFormRepository accessibleFormRepository, IValidator<EditAccessibleFormCommand> validator)
    {
        _accessibleFormRepository = accessibleFormRepository;
        _validator = validator;
    }

    public override async Task<EditAccessibleFormCommandResponse> Executor(EditAccessibleFormCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        Guard(command);

        var accessibleForm = await _accessibleFormRepository.GetById(command.Id);

        if (accessibleForm == null) throw new AccessibleFormNotFoundException();

        if (await _accessibleFormRepository.IsExistAnotherByRouteAsync(command.Id, command.Route))
            throw new RoutIsAlreadyExistException();

        accessibleForm.Update(command.Title, command.Route);

        _accessibleFormRepository.Update(accessibleForm);

        return new EditAccessibleFormCommandResponse();
    }

    private void Guard(EditAccessibleFormCommand command)
    {
        if (command.Id.IsInvalidId()) throw new IdIsInvalidIdException();
        if (command.Route.IsNullOrEmpty()) throw new RouteIsNullOrEmptyException();
        if (command.Title.IsNullOrEmpty()) throw new TitleIsNullOrEmptyException();
    }
}