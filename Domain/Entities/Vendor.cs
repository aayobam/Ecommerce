using Domain.Common;

namespace Domain.Entities;

public class Vendor : BaseEntity
{
    public string Name { get; set; }
    public string Cac { get; set; }
    public string Tin { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; } 
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public bool Approved { get; set; } = false;
     
    public virtual ICollection<ApplicationUser> Users { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
