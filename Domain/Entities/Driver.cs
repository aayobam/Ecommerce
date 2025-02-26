using Domain.Common;

namespace Domain.Entities;

public class Driver : BaseEntity
{
    public Guid LogisticId { get; set; }
    public Guid UserId { get; set; }

    public virtual Logistic Logistic { get; set; }
    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}