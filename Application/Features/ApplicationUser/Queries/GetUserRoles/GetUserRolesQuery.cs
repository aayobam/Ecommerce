using Application.DTOs.ApplicationUser;
using Application.Responses;
using MediatR;

namespace Application.Features.ApplicationUser.Queries.GetUserRoles;

public record GetUserRolesQuery(Guid userId):IRequest<GenericResponse>;
