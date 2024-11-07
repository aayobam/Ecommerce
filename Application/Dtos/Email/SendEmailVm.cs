namespace Application.DTOs.Email;

public class SendEmailVm
{
    public string Receiver { get; set; } 
    public string Subject { get; set; }
    public string Body { get; set; }
}
