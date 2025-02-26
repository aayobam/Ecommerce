using Application.Responses;
using MediatR;

namespace Application.Features.ApplicationUser.Commands.DeleteUser;

public record RemoveUserRoleCommand(Guid id) : IRequest<GenericResponse>;
