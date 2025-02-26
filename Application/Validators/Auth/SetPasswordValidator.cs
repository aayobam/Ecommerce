using Application.DTOs.Auth;
using FluentValidation;

namespace Application.Validators.Auth;

public class SetPasswordValidator : AbstractValidator<SetPasswordVm>
{
    public SetPasswordValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("{Property} is required");
        RuleFor(x => x.Code).NotEmpty().WithMessage("{Property} is required.")
            .MinimumLength(6).WithMessage("{Property} lenght cannot be less than 6.")
            .MaximumLength(6).WithMessage("{Property} length cannot be more than 6.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("{Property} is required.")
            .DependentRules(() =>
            {
                RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("{Property} is required.")
                .Equal(x => x.Password).WithMessage("Password must match");
            });
    }
}
