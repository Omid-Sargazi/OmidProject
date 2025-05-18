using OmidProject.Applications.Contracts.SmsContracts.Commands;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Messaging.Producers.RabbitMQ;
using FluentValidation;

namespace OmidProject.Applications.Application.SmsHandlers.CommandHandlers;

public class SendSmsCommandHandler : CommandHandler<SendSmsCommand, SendSmsCommandResponse>
{
    private readonly IMessageProducer _messageProducer;
    private readonly IValidator<SendSmsCommand> _validator;

    public SendSmsCommandHandler(IMessageProducer messageProducer, IValidator<SendSmsCommand> validator)
    {
        _messageProducer = messageProducer;
        _validator = validator;
    }

    public override async Task<SendSmsCommandResponse> Executor(SendSmsCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = new SendSmsCommandResponse();

        await _messageProducer.SendMessageAsync(command.Message, command.Receiver);

        return result;
    }
}