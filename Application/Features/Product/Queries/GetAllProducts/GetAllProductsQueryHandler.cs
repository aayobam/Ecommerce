using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Dtos.Product;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Application.Features.Product.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductVm>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _cache;

    public GetAllProductsQueryHandler(IMapper mapper, IProductRepository productRepository, IMemoryCache cache)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _cache = cache;
    }

    public async Task<List<ProductVm>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        var cacheKey = "products";

        var cachOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        };

        await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SetOptions(cachOptions);
            return await _productRepository.GetAllAsync();
        });
        var data = _mapper.Map<List<ProductVm>>(products);
        return data;
    }
}
