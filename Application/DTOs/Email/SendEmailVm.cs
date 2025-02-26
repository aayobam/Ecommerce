using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Email;

public class SendEmailVm
{
    public string ReceiverEmail { get; set; }
    public string EmailSubject { get; set; }
    public string HtmlEmailMessage { get; set; }
    public List<IFormFile>? Attachments { get; set; }
}
