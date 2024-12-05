using Application.DTOs.Role;
using Domain.Dtos.ApplicationUser;
using MediatR;

namespace Application.Features.ApplicationRole.Queries.GetAllRoles;

public class GetRolesQuery : IRequest<IList<RoleVm>>;
