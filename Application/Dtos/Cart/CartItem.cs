using Domain.Dtos.Product;

namespace Domain.ViewModels.Cart;

public class CartItem
{
    public ProductVm Product { get; set; }
    public decimal Total { get; set; }
}
