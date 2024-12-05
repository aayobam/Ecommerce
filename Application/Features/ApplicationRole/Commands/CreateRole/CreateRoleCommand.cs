using Application.DTOs.Role;
using MediatR;

namespace Application.Features.ApplicationRole.Commands.CreateRole;

public class CreateRoleCommand : IRequest<RoleVm>
{
    public string Name { get; set; }
}
