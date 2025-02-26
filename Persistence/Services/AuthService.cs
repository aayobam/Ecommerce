using Application.AppSettingsConfig;
using Application.Common;
using Application.Contracts.Persistence;
using Application.DTOs.Auth;
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
using Microsoft.IdentityModel.Tokens;
using Persistence.DatabaseContexts;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Persistence.Repositories;

public class AuthService : IAuthRepository
{
    private readonly ILogger<AuthService> _logger;
    public readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMediator _mediator;
    private readonly OtpSettings _otpOptionsMonitor;
    private readonly JwtSettings _jwtOptionMonitor;
    private readonly IMapper _mapper;
    private readonly EcommerceDbContext _dbContext;

    public AuthService(ILogger<AuthService> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        IMediator mediator, IOptionsMonitor<OtpSettings> otpOptionsMonitor, IOptionsMonitor<JwtSettings> jwtOptionMonitor, 
        IMapper mapper, EcommerceDbContext dbContext)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _mediator = mediator;
        _otpOptionsMonitor = otpOptionsMonitor.CurrentValue;
        _jwtOptionMonitor = jwtOptionMonitor.CurrentValue;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<GenericResponse> LoginAsync(UserLoginVm request)
    {
        _logger.LogInformation($"\n initiate user log in process | {DateTimeOffset.UtcNow} \n".ToUpper());

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new EcommerceNotFoundException("User detail is invalid, please try again with correct details.", HttpStatusCode.NotFound.ToString());
        }

        if (!user.EmailConfirmed)
        {
            // initiate account verification process.
            string code = CommonHelper.GenerateRandomDigitCode(_otpOptionsMonitor.OtpLength);

            var mailContent = new SendMailNotificationEvent
            {
                ReceiverEmail = user.Email,
                EmailSubject = $"Account Verification Otp {code}",
                HtmlEmailMessage = $"Here is your account verification code: {code} ." + "\n" +
                          "Note: This code is only valid for 10 minutes.",
                Attachments = null,
            };

            await _mediator.Publish(mailContent);

            throw new EcommerceBadRequestException($"your profile is not verified, check your email {user.Email} for verification otp", HttpStatusCode.BadRequest.ToString());
        };

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

        if (result.IsLockedOut && user.AccessFailedCount == 2)
        {
            _logger.LogInformation($"\n Account Locked after three login attempts | {DateTimeOffset.UtcNow} \n".ToUpper());
            throw new EcommerceUnauthorizedException("You exceeded the maximum failed password attempt. Kindly unlock your account by resetting your password.",HttpStatusCode.Unauthorized.ToString());
        }

        if (result.Succeeded)
        {
            _logger.LogInformation("\n Login successful | {DateTimeOffset.UtcNow} \n", DateTimeOffset.UtcNow);
            user.LockoutEnd = null;
            await _userManager.ResetAccessFailedCountAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>();

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            var token = new JwtSecurityToken(
                issuer: _jwtOptionMonitor.Issuer,
                audience: _jwtOptionMonitor.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtOptionMonitor.ExpiryInMinutes)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptionMonitor.Secret)), SecurityAlgorithms.HmacSha256)
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            var userObject = _mapper.Map<UserVm>(user);

            var tokenData = _mapper.Map<TokenDataVm>(new TokenDataVm(){ User = userObject, AccessToken = accessToken});

            return new GenericResponse()
            {
                Success = true,
                Message = "Token Fetched Successfully",
                Data = tokenData,
                StatusCode = HttpStatusCode.OK.ToString()
            };
        }
        await _userManager.AccessFailedAsync(user);
        _logger.LogInformation($"\n invalid login credentials | {DateTimeOffset.UtcNow} \n".ToUpper());
        throw new EcommerceBadRequestException("Invalid user and password combination, please try again.", HttpStatusCode.BadRequest.ToString());
    }

    public async Task<GenericResponse> LogoutAsync()
    {
        await _signInManager.SignOutAsync();

        return new GenericResponse 
        { 
            Success = true, 
            Message = "Logout successful.",
            StatusCode= HttpStatusCode.OK.ToString()
        };
    }

    public async Task<GenericResponse> ForgotPasswordAsync(ForgotPasswordVm request)
    {
        _logger.LogInformation($"\n password reset request process initiated | {DateTimeOffset.UtcNow} \n".ToUpper());

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new EcommerceNotFoundException("User with email provided does not exist.", HttpStatusCode.NotFound.ToString());
        }
        _logger.LogInformation($"\n generating otp | {DateTimeOffset.UtcNow} \n".ToUpper());

        var code = CommonHelper.GenerateRandomDigitCode(_otpOptionsMonitor.OtpLength);

        _logger.LogInformation($"\n otp generated. | {DateTimeOffset.UtcNow} \n".ToUpper());

        var mailContent = new SendMailNotificationEvent()
        {
            ReceiverEmail = user.Email,
            EmailSubject = $"Password reset Otp {code}",
            HtmlEmailMessage = $"Here is your password reset code: {code}" + "\n" +
                "Note: This code is only valid for 10 minutes.",
            Attachments = null,
        };

        _logger.LogInformation($"\n mail published to mediator | {DateTimeOffset.UtcNow} \n".ToUpper());

        _mediator.Publish(mailContent);

        return new GenericResponse()
        {
            Message = $"Password reset otp code sent to your email {user.Email}",
            Success = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
    }

    public async Task<GenericResponse> SetPasswordAsync(SetPasswordVm request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)

        if (!string.Equals(request.Password, request.ConfirmPassword, StringComparison.OrdinalIgnoreCase))
        {
            throw new EcommerceBadRequestException("passwords do not match", HttpStatusCode.BadRequest.ToString());
        }

        var encryptedOtp = CommonHelper.Encrypt(request.Code, _otpOptionsMonitor.Secret);

        if (user == null)
        {
            var otpUserObject = await _dbContext.VerificationOpts
                .Include(verificationOtp => verificationOtp.User)
                .FirstOrDefaultAsync(x => x.Code == encryptedOtp && x.User.Email == request.Email);

            _logger.LogInformation($"\n checking for otp validity | {DateTimeOffset.UtcNow} \n");

            if (otpUserObject != null)
            {
                var timeDifference = (DateTimeOffset.UtcNow - otpUserObject.Expiry).TotalMinutes;

                if (timeDifference > Convert.ToDouble(_otpOptionsMonitor.ExpiryInMinutes))
                {
                    _logger.LogInformation($"\n Otp has expired | {DateTimeOffset.UtcNow} \n");
                    throw new EcommerceBadRequestException("Otp has expired.", HttpStatusCode.BadRequest.ToString());
                }

                var hashedPassword = _userManager.PasswordHasher.HashPassword(user, request.Password);
                user.PasswordHash = hashedPassword;
                user.LockoutEnd = null;
                var result = await _userManager.UpdateAsync(user);
                await _dbContext.SaveChangesAsync();

                if (result.Succeeded)
                {
                    _dbContext.Remove(otpUserObject);
                    await _dbContext.SaveChangesAsync();

                    _logger.LogInformation($"\n verification for {user.Email} successful | {DateTimeOffset.UtcNow} \n");

                    return new GenericResponse()
                    {
                        Success = true,
                        Message = "Account verification successful, You can now proceed to login into your account.",
                        StatusCode = HttpStatusCode.Accepted.ToString(),
                    };
                }
                _logger.LogInformation($"\n Account verification failed | {DateTimeOffset.UtcNow} \n");
                throw new EcommerceBadRequestException("Account verification failed.", HttpStatusCode.BadRequest.ToString());
            }
            _logger.LogInformation($"\n invalid otp, please try again. | {DateTimeOffset.UtcNow} \n");
            throw new EcommerceNotFoundException("invalid otp, please try again.");
        }
        _logger.LogInformation($"\n account associated to this email not found. | {DateTimeOffset.UtcNow} \n");
        throw new EcommerceNotFoundException("no account associated to this email.", HttpStatusCode.NotFound.ToString());
    }
}
