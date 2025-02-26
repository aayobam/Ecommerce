using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Events;

public class SendMailNotificationEvent : INotification
{
    public string ReceiverEmail { get; set; }
    public string EmailSubject { get; set; }
    public string HtmlEmailMessage { get; set; }
    public List<IFormFile>? Attachments { get; set; }
}
