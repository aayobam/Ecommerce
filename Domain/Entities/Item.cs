using Domain.Common;

namespace Domain.Entities;

public class Item : BaseEntity
{
    public string TrackingId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public string Description { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}
