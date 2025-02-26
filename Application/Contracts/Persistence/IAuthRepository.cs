using Application.DTOs.Auth;
using Application.Responses;

namespace Application.Contracts.Persistence;

public interface IAuthRepository
{
    Task<GenericResponse> LoginAsync(UserLoginVm request);
    Task<GenericResponse> LogoutAsync();
    Task<GenericResponse> ForgotPasswordAsync(ForgotPasswordVm request);
    Task<GenericResponse> SetPasswordAsync(SetPasswordVm request);
}
