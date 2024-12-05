using Application.DTOs.Role;
using MediatR;

namespace Application.Features.ApplicationRole.Queries.GetRoleDetail;

public class GetRoleDetailQuery : IRequest<RoleVm>
{
    public Guid Id { get; set; }
}
