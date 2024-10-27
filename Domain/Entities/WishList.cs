using Domain.Common;

namespace Domain.Entities;

public class WishList : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public Guid ProductId { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
