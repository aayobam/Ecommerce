using Application.DTOs.ApplicationUser;
using FluentValidation;

namespace Application.Validators.Auth;

public class VerifyAccountValidator : AbstractValidator<VerifyUserVm>
{
    public VerifyAccountValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("{Property} is required.");
        RuleFor(x => x.Code).NotEmpty().WithMessage("{Property} is required.")
            .MinimumLength(6).WithMessage("{Property} length cannot be less than 6")
            .MaximumLength(6).WithMessage("{Property} length cannot be more than 6");
    }
}
