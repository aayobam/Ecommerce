using Domain.Dtos.Product;
using MediatR;

namespace Application.Features.Product.Queries.GetProductDetails;

public record GetProductDetailsQuery(Guid id) : IRequest<ProductVm>
{
}
