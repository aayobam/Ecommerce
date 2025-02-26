using Application.Contracts.Infrastructure;
using Application.DTOs.Email;
using Application.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EventHandlers;

public class SendMailNotificationEventHandler : INotificationHandler<SendMailNotificationEvent>
{
    private readonly ILogger<SendMailNotificationEventHandler> _logger;
    private readonly IEmailRepository _emailService;

    public SendMailNotificationEventHandler(ILogger<SendMailNotificationEventHandler> logger, IEmailRepository emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public async Task Handle(SendMailNotificationEvent notification, CancellationToken cancellationToken)
    {

        var request = new SendEmailVm()
        {
            EmailSubject = notification.EmailSubject,
            ReceiverEmail = notification.ReceiverEmail,
            HtmlEmailMessage = notification.HtmlEmailMessage,
            Attachments = notification.Attachments
        };

        var response = await _emailService.SendEmailAsync(request);

        if (response.Success)
        {
            _logger.LogInformation($"\n{response.Message} | {DateTime.UtcNow} \n".ToUpper());
        }
        _logger.LogInformation($"\n {response.Message} | {DateTime.UtcNow} \n".ToUpper());
    }
}
