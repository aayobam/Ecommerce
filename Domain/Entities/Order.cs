using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public decimal TotalAmount { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; } = DeliveryStatus.Pending;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unconfirmed;

    public Guid VendorId { get; set; }
    public Guid? DriverId { get; set; }
    public Guid CustomerId { get; set; }
    
    public virtual Vendor Vendor { get; set; }
    public virtual Driver Driver { get; set; }
    public virtual ApplicationUser Customer { get; set; }

    public string TrackingId { get; set; }
    public virtual ICollection<Item> Items { get; set; }
}
