using Application.Contracts.Persistence;
using Application.DTOs.Role;
using Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.ApplicationRole.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleVm>
{
    private readonly ILogger<CreateRoleCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRoleRepository<Domain.Entities.ApplicationRole> _roleRepository;

    public CreateRoleCommandHandler(ILogger<CreateRoleCommandHandler> logger, IMapper mapper, IRoleRepository<Domain.Entities.ApplicationRole> roleRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    public async Task<RoleVm> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateRoleCommandValidator(_roleRepository);
        var validationResult = validator.Validate(request);
        if (validationResult.Errors.Any())
        {
            _logger.LogInformation($"{request} - {request.Name} bad request");
            throw new EcommerceBadRequestException($"{request} - {request.Name} bad request", HttpStatusCode.BadRequest.ToString());
        }
        var model = _mapper.Map<Domain.Entities.ApplicationRole>(request);
        await _roleRepository.CreateAsync(model);
        var instance = _mapper.Map<RoleVm>(model);
        return instance;
    }
}
