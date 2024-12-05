using Application.DTOs.Role;
using MediatR;

namespace Application.Features.ApplicationRole.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest<RoleVm>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
