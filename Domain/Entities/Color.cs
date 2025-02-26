using Domain.Common;

namespace Domain.Entities;

public class Color : BaseEntity
{
    public string Name { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}