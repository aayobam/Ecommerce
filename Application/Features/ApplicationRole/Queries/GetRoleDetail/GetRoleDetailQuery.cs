using Application.DTOs.Role;
using MediatR;

namespace Application.Features.ApplicationRole.Queries.GetRoleDetail;

public record GetRoleDetailQuery(Guid Id) : IRequest<RoleVm>;
