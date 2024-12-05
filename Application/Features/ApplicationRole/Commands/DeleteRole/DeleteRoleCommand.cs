using MediatR;

namespace Application.Features.ApplicationRole.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
