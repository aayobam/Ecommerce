using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using Domain.Entities;
using Application.DTOs.ApplicationUser;
using Application.DTOs.Role;
using Domain.Dtos.ApplicationUser;
using AutoMapper;

namespace Application.Features.ApplicationUser.Commands.AddUserToRole;

public class AddUserToRolesCommandHandler : IRequestHandler<AddUserToRolesCommand, GenericResponse>
{
    private readonly ILogger<AddUserToRolesCommandHandler> _logger;
    private readonly IUserRepository<Domain.Entities.ApplicationUser> _userRepository;
    private readonly IRoleRepository<Domain.Entities.ApplicationRole> _roleRepository;
    private readonly IMapper _mapper;

    public AddUserToRolesCommandHandler(ILogger<AddUserToRolesCommandHandler> logger, IUserRepository<Domain.Entities.ApplicationUser> userRepository,
        IRoleRepository<Domain.Entities.ApplicationRole> roleRepository, IMapper mapper)
    {
        _logger = logger;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<GenericResponse> Handle(AddUserToRolesCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddUserToRolesCommandValidator();
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogInformation($"\n {validationResult} | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceBadRequestException(validationResult, HttpStatusCode.BadRequest.ToString());
        }

        var userInstance = await _userRepository.GetByIdAsync(request.UserId);

        if (userInstance == null)
        {
            throw new EcommerceNotFoundException("User not found", HttpStatusCode.NotFound.ToString());
        }

        var userRolesToAdd = new List<ApplicationUserRole>();

        if (request.RoleIds.Any())
        {
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
                    userRolesToAdd.Add(userRole);
                }
            }
        }

        await _userRepository.AddUserToRolesAsync(userRolesToAdd);

        var user = _mapper.Map<UserVm>(userInstance);
        
        var userRoles = new UserRolesVm
        {
            User = user,
            RolesVms = userRolesToAdd.Select(ur => new RoleVm
            {
                Id = ur.Role.Id,
                Name = ur.Role.Name,
            }).ToList()
        };

        return new GenericResponse
        {
            Success = true,
            Message = "success",
            Data = userRoles,
            StatusCode = HttpStatusCode.Created.ToString()
        };
    }
}
