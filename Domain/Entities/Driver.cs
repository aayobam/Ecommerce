using Domain.Common;

namespace Domain.Entities;

public class Driver : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual ICollection<ApplicationUser> Users { get; set; }
    public Guid LogisiticId { get; set; }
    public virtual Logistic Logistic { get; set; }
}
