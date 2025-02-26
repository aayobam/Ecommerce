using Domain.Common;

namespace Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User  { get; set; }
    public string Message { get; set; }
}
