using Domain.Dtos.Product;
using MediatR;

namespace Application.Features.Product.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<List<ProductVm>>
{
}
