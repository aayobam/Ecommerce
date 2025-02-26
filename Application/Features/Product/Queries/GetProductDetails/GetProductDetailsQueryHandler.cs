using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Dtos.Product;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Application.Features.Product.Queries.GetProductDetails;

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductVm>
{
    private readonly IApplicationLogger<GetProductDetailsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _cache;

    public GetProductDetailsQueryHandler(IApplicationLogger<GetProductDetailsQueryHandler> logger,IMapper mapper, IProductRepository productRepository, IMemoryCache cache)
    {
        _logger = logger;
        _mapper = mapper;
        _productRepository = productRepository;
        _cache = cache;
    }

    public IMapper Mapper { get; }
    public IProductRepository ProductRepository { get; }

    public async Task<ProductVm> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.id);

        if (product == null)
        {
            _logger.LogInformation($"\n {nameof(product)} - {request.id} not found | {DateTime.UtcNow} \n");
            throw new EcommerceNotFoundException("Product not found", HttpStatusCode.NotFound.ToString());
        }

        string cacheKey = "product";
        var data = _mapper.Map<ProductVm>(product);

        var cacheOption = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        };

        if (_cache.Get(cacheKey) == null)
        {
            await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetOptions(cacheOption);
                return product;
            });
        }
        _cache.Set(cacheKey, product);

        return data;
    }
}
