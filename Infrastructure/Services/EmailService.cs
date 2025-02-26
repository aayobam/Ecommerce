using Application.AppSettingsConfig;
using Application.Contracts.Infrastructure;
using Application.DTOs.Email;
using Application.Exceptions;
using Application.Responses;
using Azure;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace Infrastructure.Repositories;

public class EmailService : IEmailRepository
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IHttpClientFactory httpClientFactory, IOptionsMonitor<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.CurrentValue;
    }

    public async Task<GenericResponse> SendEmailAsync(SendEmailVm request)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var toEmail = new EmailAddress(request.ReceiverEmail);
        var fromEmail = new EmailAddress()
        {
            Email = _emailSettings.FromEmail,
            Name = _emailSettings.FromName,
        };

        var message = MailHelper.CreateSingleEmail(fromEmail, toEmail, request.EmailSubject, request.HtmlEmailMessage, request.HtmlEmailMessage);
        
        try
        {
            var response = await client.SendEmailAsync(message);

            if (response.IsSuccessStatusCode)
            {
                return new GenericResponse()
                {
                    Success = true,
                    Message = "success",
                    StatusCode = response.StatusCode.ToString(),
                };
            }
            throw new EcommerceBadRequestException("could not send mail", response.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            throw new EcommerceInternalServerErrorException($"{ex.Message} - an error occured sending mail", HttpStatusCode.InternalServerError.ToString());
        }
    }
}
