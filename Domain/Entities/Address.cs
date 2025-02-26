using Domain.Common;

namespace Domain.Entities;

public class Address : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public bool Selected { get; set; }
}
