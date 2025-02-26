using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.ApplicationRole.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
{
    private readonly ILogger<DeleteRoleCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRoleRepository<Domain.Entities.ApplicationRole> _roleRepository;

    public DeleteRoleCommandHandler(ILogger<DeleteRoleCommandHandler> logger, IMapper mapper, IRoleRepository<Domain.Entities.ApplicationRole> roleRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var instance = await _roleRepository.GetByIdAsync(request.Id);
        if (instance == null)
        {
            _logger.LogInformation($"\n {nameof(instance)} - {instance.Id} | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceNotFoundException("user not found", HttpStatusCode.NotFound.ToString());
        }
        await _roleRepository.DeleteAsync(instance);
        return Unit.Value;
    }
}
