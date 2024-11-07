using Domain.Common;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public string TrackingId { get; set; }
    public virtual ICollection<Item> Items { get; set; }
}
