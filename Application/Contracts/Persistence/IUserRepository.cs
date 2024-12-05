using Application.DTOs.ApplicationUser;
using Application.Responses;

namespace Application.Contracts.Persistence;

public interface IUserRepository<T> : IGenericRepository<T> where T : class
{
    Task<GenericResponse> VerifyUserAsync(VerifyUserVm request);
    Task<bool> IsEmailAddressUniqueAsync(string emailAddress);
    Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber);
}
