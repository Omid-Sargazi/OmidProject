using System.Net;
using OmidProject.Applications.Contracts.Repository;
using OmidProject.Applications.Contracts.SystemMessageContracts.Commands;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Application.SystemMessageHandlers.CommandHandlers;

public class DeleteSystemMessageByLanguageCommandHandler
    : CommandHandler<DeleteSystemMessageByLanguageCommand, CommandResponse>
{
    private readonly ISystemMessageRepository _systemErrorRepository;

    public DeleteSystemMessageByLanguageCommandHandler(ISystemMessageRepository systemErrorRepository)
    {
        _systemErrorRepository = systemErrorRepository;
    }

    public override async Task<CommandResponse> Executor(DeleteSystemMessageByLanguageCommand command, CancellationToken cancellationToken)
    {
        var found = await _systemErrorRepository.GetMessageByCodeAndType(command.Code, command.TypeMessage);

        //if (found == null) throw new SystemErrorCanNotFoundException();
        if (found == null)
            throw new ApplicationException("ارور ناشناخته ای رخ داد ")
            {
                Data = { { "StatusCode", HttpStatusCode.InternalServerError } }
            };


        found.DeleteMessageByLanguage(command.ListLanguage);

        await _systemErrorRepository.Update(found);

        return CommandResponseSuccessful.CreateSuccessful();
    }
}