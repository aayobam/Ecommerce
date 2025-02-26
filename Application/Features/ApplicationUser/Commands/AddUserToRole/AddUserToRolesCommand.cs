using Application.Responses;
using MediatR;

namespace Application.Features.ApplicationUser.Commands.AddUserToRole;

public class AddUserToRolesCommand : IRequest<GenericResponse>
{
    public Guid UserId { get; set; }
    public ICollection<Guid> RoleIds { get; set; }
}
