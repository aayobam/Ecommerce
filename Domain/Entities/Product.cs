using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{  
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public int Quantity { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public bool Available { get; set; } = true;

    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public Guid VendorId { get; set; }
    public virtual Vendor Vendor { get; set; }

    public virtual ICollection<WishList> WishLists { get; set; }
    public virtual ICollection<Color> Colors { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
}
