using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using Application.DTOs.Category;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.Category.Queries.GetAllCategory;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryVm>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _cache;
    private readonly ILogger<GetCategoriesQueryHandler> logger;

    public GetCategoriesQueryHandler(ILogger<GetCategoriesQueryHandler> _logger, IMapper mapper, ICategoryRepository categoryRepository, IMemoryCache cache)
    {
        _logger = logger;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _cache = cache;
    }

    public async Task<List<CategoryVm>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = "categories";

        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        };

        var categories = await _categoryRepository.GetAllAsync();
        var cachedData = _cache.Get(cacheKey);
        
        if (cachedData == null)
        {
            await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetOptions(cacheEntryOptions);
                return categories;
            });
        }
        _cache.Set(cacheKey, categories, cacheEntryOptions);

        var data = _mapper.Map<List<CategoryVm>>(categories);
        return data;
    }
}
