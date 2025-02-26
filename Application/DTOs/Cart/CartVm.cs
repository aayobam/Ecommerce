namespace Domain.ViewModels.Cart;

public class CartVm
{
    private decimal _sumTotal;
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public List<CartItem> Products { get; set; }
    public decimal SumTotal {
        get { return _sumTotal; }
        set 
        {
            foreach (var item in Products)
            {
                _sumTotal += item.Total;
            }
        }
    }
}
