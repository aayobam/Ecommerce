using MediatR;

namespace Application.Features.ApplicationRole.Commands.DeleteRole;

public record DeleteRoleCommand(Guid Id) : IRequest<Unit>;