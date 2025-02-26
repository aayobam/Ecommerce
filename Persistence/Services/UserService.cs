using Application.AppSettingsConfig;
using Application.Common;
using Application.Contracts.Persistence;
using Application.DTOs.ApplicationUser;
using Application.Exceptions;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.DatabaseContexts;
using System.Net;

namespace Persistence.Repositories;

public class UserService<T> : IUserRepository<T> where T : ApplicationUser
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly EcommerceDbContext _context;
    private readonly ILogger<UserService<T>> _logger;
    private readonly OtpSettings _otpOptionMonitor;
    private readonly IMapper _mapper;

    public UserService(ILogger<UserService<T>> logger,UserManager<ApplicationUser> userManager, EcommerceDbContext context, 
        IOptionsMonitor<OtpSettings> otpOptionMonitor, IMapper mapper)
    {
        _userManager = userManager;
        _context = context;
        _logger = logger;
        _otpOptionMonitor = otpOptionMonitor.CurrentValue;
        _mapper = mapper;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
           await _userManager.CreateAsync(entity);
        }
    }

    public async Task AddUserToRolesAsync(List<ApplicationUserRole> entities)
    {
        await _context.UserRoles.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _userManager.CreateAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        await _userManager.DeleteAsync(entity);
    }

    public async Task DeleteRangeAsync(List<T> entities)
    {
        foreach (T entity in entities)
        {
            await _userManager.DeleteAsync(entity);
        }
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> IsEmailAddressUniqueAsync(string emailAddress)
    {
        var instance =  await _userManager.FindByEmailAsync(emailAddress);
        if (instance == null)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber)
    {
        var instance = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        
        if (instance == null)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> RemoveUserFromRolesAsync(List<ApplicationUserRole> entities)
    {
        _context.UserRoles.RemoveRange(entities);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task UpdateAsync(T entity)
    {
        await _userManager.UpdateAsync(entity);
    }

    public async Task UpdateRangeAsync(List<T> entities)
    {
        foreach (T entity in entities)
        {
            await _userManager.UpdateAsync(entity);
        }
    }

    public async Task<IReadOnlyList<ApplicationUserRole>> GetUserRolesAsync(T entity)
    {
        return await _context.UserRoles.AsNoTracking().Where(x => x.UserId == entity.Id).ToListAsync();
    }

    public async Task<GenericResponse> VerifyUserAsync(VerifyUserVm request)
    {
        _logger.LogInformation($"\n Account verification process started | {DateTimeOffset.UtcNow} \n");

        try
        {
            _logger.LogInformation($"\n encrypting otp | {DateTimeOffset.UtcNow} \n");

            string encryptedOtp = CommonHelper.Encrypt(request.Code, _otpOptionMonitor.Secret);

            var otpUserObject = await _context.VerificationOpts
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Code == encryptedOtp && x.User.Email == request.Email);

            _logger.LogInformation($"\n checking for otp validity | {DateTimeOffset.UtcNow} \n");

            if (otpUserObject != null)
            {
                var timeDifference = (DateTimeOffset.UtcNow - otpUserObject.Expiry).TotalMinutes;

                if (timeDifference > Convert.ToDouble(_otpOptionMonitor.ExpiryInMinutes))
                {
                    _logger.LogInformation($"\n Otp has expired | {DateTimeOffset.UtcNow} \n");
                    throw new EcommerceBadRequestException("Otp has expired.", HttpStatusCode.BadRequest.ToString());
                }

                var user = _mapper.Map<T>(otpUserObject.User);
                user.EmailConfirmed = true;
                var response = await _userManager.UpdateAsync(user);

                if (response.Succeeded)
                {
                    _context.Remove(otpUserObject);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"\n verification for {user.Email} successful | {DateTimeOffset.UtcNow} \n".ToUpper());

                    return new GenericResponse()
                    {
                        Success = true,
                        Message = "Account verification successful, You can now proceed to login into your account.",
                        StatusCode = HttpStatusCode.Accepted.ToString(),
                    };
                }
                _logger.LogInformation($"\n Account verification failed | {DateTimeOffset.UtcNow} \n".ToUpper());
                throw new EcommerceBadRequestException("Account verification failed.", HttpStatusCode.BadRequest.ToString());
            }
            _logger.LogInformation($"\n invalid otp, please try again. | {DateTimeOffset.UtcNow} \n".ToUpper());
            throw new EcommerceNotFoundException("invalid otp, please try again.");
        }
        catch (Exception exception)
        {
            throw new EcommerceBadRequestException($"could not complete operation - {exception.Message}", HttpStatusCode.InternalServerError.ToString());
        }   
    }
}
