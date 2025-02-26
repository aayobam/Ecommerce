using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.ApplicationRole.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IRoleRepository<Domain.Entities.ApplicationRole> _roleRepository;

    public CreateRoleCommandValidator(IRoleRepository<Domain.Entities.ApplicationRole> roleRepository)
    {
        _roleRepository = roleRepository;

        RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(x => x).MustAsync(IsRoleNameUnique).WithMessage("{PropertyName} already exist.");
    }

    private async Task<bool> IsRoleNameUnique(CreateRoleCommand command, CancellationToken token)
    {
        return await _roleRepository.IsRoleNameUniqueAsync(command.Name);
    }
}
