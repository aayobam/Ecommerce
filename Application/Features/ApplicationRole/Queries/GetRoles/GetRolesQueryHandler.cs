using Application.Contracts.Persistence;
using Application.DTOs.Role;
using Application.Features.ApplicationRole.Commands.CreateRole;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.ApplicationRole.Queries.GetAllRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IList<RoleVm>>
{
    private readonly ILogger<CreateRoleCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRoleRepository<Domain.Entities.ApplicationRole> _roleRepository;

    public GetRolesQueryHandler(ILogger<CreateRoleCommandHandler> logger, IMapper mapper, IRoleRepository<Domain.Entities.ApplicationRole> roleRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    public async Task<IList<RoleVm>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync();
        var data = _mapper.Map<List<RoleVm>>(roles);
        return data;
    }
}
