using Application.DTOs.ApplicationUser;
using Application.Responses;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IUserRepository<T> : IGenericRepository<T> where T : class
{
    Task<GenericResponse> VerifyUserAsync(VerifyUserVm request);
    Task<bool> IsEmailAddressUniqueAsync(string emailAddress);
    Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber);
    Task AddUserToRolesAsync(List<ApplicationUserRole> entities);
    Task<IReadOnlyList<ApplicationUserRole>> GetUserRolesAsync(T entity);
    Task<bool> RemoveUserFromRolesAsync(List<ApplicationUserRole> Entities);
}
