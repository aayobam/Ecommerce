using Application.AppSettingsConfig;
using Application.Common;
using Application.Contracts.Persistence;
using Application.DTOs.ApplicationUser;
using Application.DTOs.Role;
using Application.Events;
using Application.Exceptions;
using Application.Responses;
using AutoMapper;
using Domain.Dtos.ApplicationUser;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace Application.Features.ApplicationUser.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GenericResponse>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IUserRepository<Domain.Entities.ApplicationUser> _userRepository;
    private readonly IRoleRepository<Domain.Entities.ApplicationRole> _roleRepository;
    private readonly IMapper _mapper;
    private readonly OtpSettings _otpOptionMonitor;
    private readonly IMediator _mediator;

    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUserRepository <Domain.Entities.ApplicationUser>userRepository,
        IRoleRepository<Domain.Entities.ApplicationRole> roleRepository, IMapper mapper, 
        IOptionsMonitor<OtpSettings> otpOptionMonitor, IMediator mediator)
    {
        _logger = logger;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
        _otpOptionMonitor = otpOptionMonitor.CurrentValue;
        _mediator = mediator;
    }

    public async Task<GenericResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserCommandValidator(_userRepository);
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogInformation($"\n {validationResult} | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceBadRequestException(validationResult, HttpStatusCode.BadRequest.ToString());
        }

        var existingUser = await _userRepository.IsEmailAddressUniqueAsync(request.EmailAddress);

        if (existingUser != null)
        {
            _logger.LogInformation("\n user already exists | {DateTimeOffset.UtcNow} \n", DateTimeOffset.UtcNow);
            throw new EcommerceBadRequestException("User with the provided information already exists.", HttpStatusCode.BadRequest.ToString());
        }

        _logger.LogInformation($"\n Creating user | {DateTimeOffset.UtcNow} \n");

        var userObject = new Domain.Entities.ApplicationUser()
        {
            Email = request.EmailAddress,
            PhoneNumber = request.PhoneNumber,
            EmailConfirmed = true,
            NormalizedEmail = request.EmailAddress.ToUpper(),
            UserName = request.EmailAddress,
            NormalizedUserName = request.EmailAddress.ToUpper(),
            TwoFactorEnabled = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        PasswordHasher<Domain.Entities.ApplicationUser> passwordHasher = new PasswordHasher<Domain.Entities.ApplicationUser>();
        var hashedPassword = passwordHasher.HashPassword(userObject, request.Password);
        userObject.PasswordHash = hashedPassword;

        List<ApplicationUserRole> userRolesToAdd = new List<ApplicationUserRole>();

        _logger.LogInformation($"\n Adding user to role | {DateTimeOffset.UtcNow} \n", DateTimeOffset.UtcNow);
        
        if (request.RoleIds.Any())
        {
            foreach (var RoleId in request.RoleIds)
            {
                var role = await _roleRepository.GetByIdAsync(RoleId);

                if (role != null)
                {
                    var userRole = new ApplicationUserRole()
                    {
                        UserId = userObject.Id,
                        RoleId = role.Id
                    };
                    userRolesToAdd.Add(userRole);
                }
            }
        }
        await _userRepository.CreateAsync(userObject);
        await _userRepository.AddUserToRolesAsync(userRolesToAdd);

        var code = CommonHelper.GenerateRandomDigitCode(_otpOptionMonitor.OtpLength);

        _logger.LogInformation($"\n otp generated. | {DateTimeOffset.UtcNow} \n".ToUpper());

        var mailContent = new SendMailNotificationEvent()
        {
            ReceiverEmail = request.EmailAddress,
            EmailSubject = $"Verify your new account",
            HtmlEmailMessage = $"Here is your account verificat code: {code}" + "\n" +
                "Note: This code is only valid for 10 minutes.",
            Attachments = null,
        };

        _mediator.Publish(mailContent);

        var user = _mapper.Map<UserVm>(request);

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
