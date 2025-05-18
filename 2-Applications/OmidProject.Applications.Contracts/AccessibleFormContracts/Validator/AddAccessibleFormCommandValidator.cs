using OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;
using OmidProject.Applications.Contracts.Repository;
using OmidProject.Domains.Domain.General;
using FluentValidation;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Validator;

public class AddAccessibleFormCommandValidator : AbstractValidator<AddAccessibleFormCommand>
{
    public AddAccessibleFormCommandValidator(IGenericRepository<AccessibleForm, int> genericRepository)
    {
        RuleFor(accessibleForm => accessibleForm.Title)
            .NotEmpty().WithMessage("عنوان نمیتواند خالی باشد")
            .WithMessage("عنوان فرم دسترسی قبلاً ثبت شده است");
    }
}