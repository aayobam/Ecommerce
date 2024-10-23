using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public bool Available { get; set; } = true;
}
