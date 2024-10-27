using Application.DTOs.Category;
using FluentValidation;

namespace Application.Validators.Category;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryVm>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("{PropertyName} is required")
            .MaximumLength(70).WithMessage("{PropertyName} cannot me more than 70 characters");
    }
}
