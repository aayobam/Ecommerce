using Application.AppSettingsConfig;
using Application.Contracts.Infrastructure;
using Application.DTOs.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace Infrastructure.Repositories;

public class EmailService : IEmailRepository
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IHttpClientFactory httpClientFactory, IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmailAsync(SendEmailVm request)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var to = new EmailAddress(request.Receiver);
        var from = new EmailAddress()
        {
            Email = _emailSettings.FromEmail,
            Name = _emailSettings.FromName,
        };
        var message = MailHelper.CreateSingleEmail(from, to, request.Subject, request.Body, request.Body);
        var response = await client.SendEmailAsync(message);
        return response.IsSuccessStatusCode;
    }
}
