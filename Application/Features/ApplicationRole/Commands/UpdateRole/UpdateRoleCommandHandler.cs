using Application.Contracts.Persistence;
using Application.DTOs.Role;
using Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.ApplicationRole.Commands.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleVm>
{
    private readonly ILogger<UpdateRoleCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRoleRepository<Domain.Entities.ApplicationRole> _roleRepository;

    public UpdateRoleCommandHandler(ILogger<UpdateRoleCommandHandler> logger, IMapper mapper, IRoleRepository<Domain.Entities.ApplicationRole> roleRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    public async Task<RoleVm> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var model = await _roleRepository.GetByIdAsync(request.Id);
        if (model == null)
        {
            _logger.LogInformation($"\n {nameof(model)} - {model.Id} | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceNotFoundException("Role not found", HttpStatusCode.NotFound.ToString());
        }
        var validator = new UpdateRoleCommandValidator(_roleRepository);
        var validationResult = validator.Validate(request);
        if (validationResult.Errors.Any())
        {
            _logger.LogInformation($"\n {validationResult} | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceBadRequestException(validationResult, HttpStatusCode.BadRequest.ToString());
        }
        model.Name = request.Name;
        await _roleRepository.UpdateAsync(model);
        var instance = _mapper.Map<RoleVm>(model);
        return instance;
    }
}
