using Application.Responses;
using MediatR;

namespace Application.Features.ApplicationUser.Commands.RemoveUserRoles;

public record RemoveUserRoleCommand(Guid userId) : IRequest<GenericResponse>
{
    public List<Guid> RoleIds{ get; set; }
}
