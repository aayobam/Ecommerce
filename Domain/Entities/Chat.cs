using Domain.Common;

namespace Domain.Entities;

public class Chat : BaseEntity
{
    public string Reason { get; set; }
    public Guid SenderId { get; set; }
    public ApplicationUser Sender { get; set; }
    public Guid ReceiverId { get; set; }
    public ApplicationUser Receiver { get; set; }
    public string Message { get; set; }
}
