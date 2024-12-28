using Application.DTOs.Email;

namespace Application.Contracts.Infrastructure;

public interface IEmailRepository
{
    Task<bool> SendEmailAsync(SendEmailVm request);
}
