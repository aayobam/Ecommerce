using Application.Contracts.Persistence;
using Application.DTOs.Role;
using Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.ApplicationRole.Queries.GetRoleDetail;

public class GetRoleDetailQueryHandler : IRequestHandler<GetRoleDetailQuery, RoleVm>
{
    private readonly ILogger<GetRoleDetailQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRoleRepository<Domain.Entities.ApplicationRole> _roleRepository;

    public GetRoleDetailQueryHandler(ILogger<GetRoleDetailQueryHandler> logger, IMapper mapper, IRoleRepository<Domain.Entities.ApplicationRole> roleRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    public async Task<RoleVm> Handle(GetRoleDetailQuery request, CancellationToken cancellationToken)
    {
        var instance = await _roleRepository.GetByIdAsync(request.Id);
        if (instance == null)
        {
            _logger.LogInformation($"{nameof(instance)} - {request.Id} not found | {DateTimeOffset.Now}");
            throw new EcommerceNotFoundException("Role not found", HttpStatusCode.NotFound.ToString());
        }
        var data = _mapper.Map<RoleVm>(instance);
        return data;
    }
}
