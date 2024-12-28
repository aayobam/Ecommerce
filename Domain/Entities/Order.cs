using Domain.Common;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public decimal Total { get; set; }
    public bool PaymentStatus { get; set; }  = false;
    public string TrackingId { get; set; }
    public bool DeliveryStatus { get; set; } = false;
    public Guid? UserId { get; set; }
    public Guid? DriverId { get; set; }
    public Guid? VendorId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public Driver Driver { get; set; }
    public virtual Vendor Vendor { get; set; }
    public virtual ICollection<Item> Items { get; set; }
}
