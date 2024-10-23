
namespace Domain.Entities;

public class Cart
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public List<Product> Products { get; set; }
    public decimal Total { get; set; }
    public decimal SumTotal { get; set; }
}
