using Application.DTOs.Email;
using Application.Responses;

namespace Application.Contracts.Infrastructure;

public interface IEmailRepository
{
    Task<GenericResponse> SendEmailAsync(SendEmailVm request);
}
