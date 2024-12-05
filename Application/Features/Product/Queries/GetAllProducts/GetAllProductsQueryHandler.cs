using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Dtos.Product;
using MediatR;

namespace Application.Features.Product.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductVm>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<List<ProductVm>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        var data = _mapper.Map<List<ProductVm>>(products);
        return data;
    }
}
