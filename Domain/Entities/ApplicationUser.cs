using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateModified { get; set; } = DateTime.UtcNow;
    public ICollection<Address> Addresses { get; set; }
    public ICollection<Vendor> Vendors{ get; set; }
}
