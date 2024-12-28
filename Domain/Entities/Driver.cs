using Domain.Common;

namespace Domain.Entities;

public class Driver : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid LogisticId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual Logistic Logistic { get; set; }
    public ICollection<Order> Orders { get; set; }
}
