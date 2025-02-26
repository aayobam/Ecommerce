using Domain.Common;

namespace Domain.Entities;

public class Vendor : BaseEntity
{
    public string BusinessName { get; set; }
    public string Cac { get; set; }
    public string Tin { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public bool Verified { get; set; } = false;

    public Guid VendorAdminId { get; set; }
    public virtual ApplicationUser vendorAdmin { get; set; }

    public ICollection<Order> Orders { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    public virtual ICollection<ApplicationUser> Users { get; set; }
}
