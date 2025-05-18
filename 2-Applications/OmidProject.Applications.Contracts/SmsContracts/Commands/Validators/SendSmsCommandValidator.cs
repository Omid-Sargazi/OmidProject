using FluentValidation;

namespace OmidProject.Applications.Contracts.SmsContracts.Commands.Validators;

public class SendSmsCommandValidator : AbstractValidator<SendSmsCommand>
{
    public SendSmsCommandValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage("پیغام نامعتبر است");

        RuleFor(x => x.Receiver)
            .NotEmpty()
            .WithMessage("شماره تلفن نامعتبر است");
    }
}