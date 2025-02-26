using Domain.Common;

namespace Domain.Entities;

public class Logistic : BaseEntity
{
    public string Name { get; set; }
    public string Cac { get; set; }
    public string Tin { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Lga { get; set; }
    public string PostalCode { get; set; }
    public string State { get; set; }
    public string Country { get; set; } = "Nigeria";
    public virtual ICollection<Driver> Drivers { get; set; }
    public virtual ICollection<ApplicationUser> Users { get; set; }
}
