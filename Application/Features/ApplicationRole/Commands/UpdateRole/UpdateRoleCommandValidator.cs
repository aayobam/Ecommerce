using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.ApplicationRole.Commands.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    private readonly IRoleRepository<Domain.Entities.ApplicationRole> _roleRepository;

    public UpdateRoleCommandValidator(IRoleRepository<Domain.Entities.ApplicationRole> roleRepository)
    {
        _roleRepository = roleRepository;

        RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required.");
        RuleFor(x => x).MustAsync(IsRoleNameUnique).WithMessage("{PropertyName} already exist.");
    }

    private async Task<bool> IsRoleNameUnique(UpdateRoleCommand command, CancellationToken token)
    {
        return await _roleRepository.IsRoleNameUniqueAsync(command.Name);
    }
}
