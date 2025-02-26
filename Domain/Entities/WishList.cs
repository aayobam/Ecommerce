using Domain.Common;

namespace Domain.Entities;

public class WishList : BaseEntity
{
    public Guid CustomerId { get; set; }
    public virtual ApplicationUser Customer { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}
