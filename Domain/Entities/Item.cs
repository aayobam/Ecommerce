using Domain.Common;

namespace Domain.Entities;

public class Item : BaseEntity
{
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
    public string TrackingId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public bool DeliveryStatus { get; set; } = false;
}
