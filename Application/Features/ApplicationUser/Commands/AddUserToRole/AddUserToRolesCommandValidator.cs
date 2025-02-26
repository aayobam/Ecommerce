using FluentValidation;

namespace Application.Features.ApplicationUser.Commands.AddUserToRole;

public class AddUserToRolesCommandValidator:AbstractValidator<AddUserToRolesCommand>
{
    public AddUserToRolesCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(x => x.RoleIds).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
