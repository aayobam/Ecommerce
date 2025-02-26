using Application.DTOs.Auth;
using FluentValidation;

namespace Application.Validators.Auth;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordVm>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email).NotNull().WithMessage("Email is required.");
    }
}
