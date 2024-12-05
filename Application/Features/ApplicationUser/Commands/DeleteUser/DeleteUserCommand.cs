using MediatR;

namespace Application.Features.ApplicationUser.Commands.DeleteUser;

public record DeleteUserCommand(Guid id) : IRequest<Unit>;
