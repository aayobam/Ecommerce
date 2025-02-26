using Domain.Dtos.Product;

namespace Domain.ViewModels.Cart;

public class CartItem
{
    private decimal _total;
    public ProductVm Product { get; set; }
    public decimal Total 
    {
        get 
        {
            return _total;
        }
        set
        {
            _total = this.Product.Price * this.Product.Quantity;
        }
    }
}
