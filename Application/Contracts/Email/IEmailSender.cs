using Application.DTOs.Email;

namespace Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(SendEmailVm request);
}
