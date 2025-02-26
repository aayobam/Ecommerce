using Domain.Common;

namespace Domain.Entities;

public class AuditTrail : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public string Action { get; set; }
    public string Reason { get; set; }
    public string Source { get; set; }
    public string Platform { get; set; }
    public string Status { get; set; }
}
