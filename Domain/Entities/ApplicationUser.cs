using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; }
    public virtual ICollection<Address>? Addresses { get; set; }
    public virtual ICollection<Vendor>? Vendors{ get; set; }
    public virtual ICollection<Order>? Orders { get; set; }

    public Guid RoleId { get; set; }
    public virtual ApplicationRole? Role { get; set; }

    public Guid DriverId { get; set; }
    public virtual Driver? Driver { get; set; }

    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset DateModified { get; set; }
}
