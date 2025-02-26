using Domain.Common;

namespace Domain.Dtos.Product;

public class ProductVm : BaseEntity
{
    public string Name { get; set; }
    public Decimal Price { get; set; }
    public int Quantity { get; set; }
}
