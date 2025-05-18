using OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository;
using OmidProject.Domains.Domain.General;
using FluentValidation;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Validator;

public class EditAccessibleFormCommandValidator : AbstractValidator<EditAccessibleFormCommand>
{
    public EditAccessibleFormCommandValidator(IGenericRepository<AccessibleForm, int> genericRepository)
    {
        // بررسی اینکه ID در دیتابیس وجود دارد یا خیر
        RuleFor(accessibleForm => accessibleForm.Id)
            .NotEmpty()
            .WithMessage("آیدی نمی‌تواند خالی باشد")
            .MustAsync(async (id, cancellationToken) =>
                await genericRepository.ExistsAsync(form => form.Id == id, cancellationToken))
            .WithMessage("فرم با این آیدی یافت نشد");

        RuleFor(accessibleForm => accessibleForm.Route)
            .NotEmpty()
            .WithMessage("عنوان نمی‌تواند خالی باشد");
    }
}