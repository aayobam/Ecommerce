using FluentValidation;

namespace Application.Features.ApplicationUser.Commands.RemoveUserRoles;

public class RemoveUserRolesValidator:AbstractValidator<RemoveUserRoleCommand>
{
    public RemoveUserRolesValidator()
    {
        RuleFor(x => x.userId).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(x => x.RoleIds).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
    }
}
