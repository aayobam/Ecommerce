using Application.DTOs.ApplicationUser;
using Application.Responses;

namespace Application.Contracts.Persistence;

public interface IAuthRepository
{
    Task<GenericResponse> LoginAsync(UserLoginVm request);
    Task<GenericResponse> LogoutAsync();
    Task<GenericResponse> VerifyUserAsync(VerifyUserVm request);
    Task<GenericResponse> RequestPasswordReset(PasswordResetVm request);
    Task<GenericResponse> SetPasswordAsync(SetPasswordVm request);
}
