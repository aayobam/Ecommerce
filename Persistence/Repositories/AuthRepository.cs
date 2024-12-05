using Application.Contracts.Persistence;
using Application.DTOs.ApplicationUser;
using Application.Responses;

namespace Persistence.Repositories;

public class AuthRepository : IAuthRepository
{
    public Task<GenericResponse> LoginAsync(UserLoginVm request)
    {
        throw new NotImplementedException();
    }

    public Task<GenericResponse> LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GenericResponse> RequestPasswordReset(PasswordResetVm request)
    {
        throw new NotImplementedException();
    }

    public Task<GenericResponse> SetPasswordAsync(SetPasswordVm request)
    {
        throw new NotImplementedException();
    }
}
