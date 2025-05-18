using OmidProject.Applications.Application.SystemMessageHandlers.Exceptions;
using OmidProject.Applications.Contracts.Repository;
using OmidProject.Applications.Contracts.SystemMessageContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Domains.Domain.SystemMessages;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.SystemMessageHandlers.CommandHandlers;

public class CreateSystemMessageCommandHandler
    : CommandHandler<CreateSystemMessageCommand, CommandResponse>
{
    private readonly ISystemMessageRepository _systemErrorRepository;
    private readonly UserManager<ApplicationUser> _userManager;


    public CreateSystemMessageCommandHandler(ISystemMessageRepository systemErrorRepository,
        UserManager<ApplicationUser> userManager)
    {
        _systemErrorRepository = systemErrorRepository;
        _userManager = userManager;
    }

    public override async Task<CommandResponse> Executor(CreateSystemMessageCommand command, CancellationToken cancellationToken)
    {
        var found = await _systemErrorRepository.GetMessageByCodeAndType(command.Code, command.TypeMessage);

        if (found != null) throw new SystemErrorCodeIsDuplicateException();

        var list = new List<SystemDataMessage>();

        if (command.List.IsEmpty()) throw new SystemErrorMessageIsEmptyException();

        foreach (var item in command.List)
        {
            var value = new SystemDataMessage(item.MessageLanguage, item.Prefix, item.Message);

            list.Add(value);
        }

        var systemError = new Domains.Domain.SystemMessages.SystemMessage(command.Code, command.TypeMessage, list);

        await _systemErrorRepository.Create(systemError);

        return CommandResponseSuccessful.CreateSuccessful();
    }
}