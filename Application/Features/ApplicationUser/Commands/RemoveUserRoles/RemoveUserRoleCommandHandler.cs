using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.ApplicationUser.Commands.RemoveUserRoles;

public class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand, GenericResponse>
{
    private readonly ILogger<RemoveUserRoleCommandHandler> _logger;
    private readonly IUserRepository<Domain.Entities.ApplicationUser> _userRepository;
    private readonly IRoleRepository<Domain.Entities.ApplicationRole> _roleRepository;

    public RemoveUserRoleCommandHandler(ILogger<RemoveUserRoleCommandHandler> logger, IUserRepository<Domain.Entities.ApplicationUser> userRepository,
        IRoleRepository<Domain.Entities.ApplicationRole> roleRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<GenericResponse> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        var validator = new RemoveUserRolesValidator();
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogInformation($"\n {validationResult} | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceBadRequestException(validationResult, HttpStatusCode.BadRequest.ToString());
        }

        var userInstance = await _userRepository.GetByIdAsync(request.userId);

        if (userInstance == null)
        {
            throw new EcommerceNotFoundException("User not found", HttpStatusCode.NotFound.ToString());
        }

        var userRolesToRemove = new List<ApplicationUserRole>();

        foreach (var RoleId in request.RoleIds)
        {
            var role = await _roleRepository.GetByIdAsync(RoleId);

            if (role != null)
            {
                var userRole = new ApplicationUserRole()
                {
                    UserId = userInstance.Id,
                    RoleId = role.Id
                };
                userRolesToRemove.Add(userRole);
            }
        }

        await _userRepository.RemoveUserFromRolesAsync(userRolesToRemove);

        return new GenericResponse()
        {
            Success = true,
            Message = "success",
            StatusCode = HttpStatusCode.OK.ToString(),
        };
    }
}
