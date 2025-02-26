using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }

    public Guid DriverId { get; set; }
    public Guid VendorId { get; set; }
    public Guid LogisticId { get; set; }

    public virtual Driver Driver { get; set; }
    public virtual Vendor Vendor { get; set; }
    public virtual Logistic Logistic { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<Address> Addresses { get; set; }
    public virtual ICollection<WishList> WishLists { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset DateModified { get; set; }
}
