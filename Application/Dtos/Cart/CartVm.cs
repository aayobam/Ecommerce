namespace Domain.ViewModels.Cart;

public class CartVm
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public List<CartItem> Products { get; set; }
    public decimal SumTotal { get; set; }
}
