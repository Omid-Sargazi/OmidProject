using OmidProject.Applications.Application.SystemMessageHandlers.Exceptions;
using OmidProject.Applications.Contracts.Repository;
using OmidProject.Applications.Contracts.SystemMessageContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Domains.Domain.SystemMessages;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.SystemMessageHandlers.CommandHandlers;

public class UpdateSystemMessageCommandHandler
    : CommandHandler<UpdateSystemMessageCommand, CommandResponse>
{
    private readonly ISystemMessageRepository _systemErrorRepository;
    private readonly UserManager<ApplicationUser> _userManager;


    public UpdateSystemMessageCommandHandler(ISystemMessageRepository systemErrorRepository,
        UserManager<ApplicationUser> userManager)
    {
        _systemErrorRepository = systemErrorRepository;
        _userManager = userManager;
    }

    public override async Task<CommandResponse> Executor(UpdateSystemMessageCommand command, CancellationToken cancellationToken)
    {
        var found = await _systemErrorRepository.GetMessageByCodeAndType(command.Code, command.TypeMessage);

        if (found == null) throw new SystemErrorCanNotFoundException();

        var list = new List<SystemDataMessage>();

        foreach (var item in command.List)
        {
            var mess = new SystemDataMessage(item.MessageLanguage, item.Prefix, item.Message);

            list.Add(mess);
        }

        found.UpdateMessage(list);

        await _systemErrorRepository.Update(found);

        return CommandResponseSuccessful.CreateSuccessful();
    }
}