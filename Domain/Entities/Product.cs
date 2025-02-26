using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public int Quantity { get; set; } = 0;
    public bool InStock { get; set; }
    public int Discount { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string MemorySize { get; set; }
    public decimal? Height { get; set; } 
    public decimal? Width { get; set; }
    public decimal? Weight { get; set; }

    public Guid CategoryId { get; set; }
    public Guid VendorId { get; set; }
    public Guid WishListId { get; set; }

    public virtual Category Category { get; set; }
    public virtual Vendor Vendor { get; set; }
    public virtual WishList WishList { get; set; }

    public virtual ICollection<Image> Images { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<Color>? Colors { get; set; }
}
