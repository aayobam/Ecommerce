using Application.Contracts.Persistence;
using Application.Responses;
using Domain.Dtos.ApplicationUser;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System;
using Application.Exceptions;
using Application.DTOs.ApplicationUser;
using Application.DTOs.Role;
using AutoMapper;

namespace Application.Features.ApplicationUser.Queries.GetUserRoles;

public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, GenericResponse>
{
    private readonly ILogger<GetUserRolesQueryHandler> _logger;
    private readonly IUserRepository<Domain.Entities.ApplicationUser> _userRepository;
    private readonly IMapper _mapper;

    public GetUserRolesQueryHandler(ILogger<GetUserRolesQueryHandler> logger, IUserRepository<Domain.Entities.ApplicationUser> userRepository, IMapper mapper)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GenericResponse> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);

        if (user == null)
        {
            _logger.LogError($"\n user with {request.userId} not found | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceNotFoundException($" user with {request.userId} not found.");
        }

        var userRoles = await _userRepository.GetUserRolesAsync(user);

        var userRoleList = new UserRolesVm()
        {
            User = _mapper.Map<UserVm>(user),
            RolesVms = userRoles.Select(x => new RoleVm()
            {
                Name = x.Role.Name,
            }).ToList()
        };

        return new GenericResponse()
        {
            Success = true,
            Message = $"roles for {user} fetched successfully",
            Data = userRoleList,
            StatusCode = HttpStatusCode.OK.ToString()
        };
    }
}
