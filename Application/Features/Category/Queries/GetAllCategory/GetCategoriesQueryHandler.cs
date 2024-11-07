using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using Application.DTOs.Category;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Queries.GetAllCategory;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryVm>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IApplicationLogger<GetCategoriesQueryHandler> logger;

    public GetCategoriesQueryHandler(IApplicationLogger<GetCategoriesQueryHandler> _logger,IMapper mapper, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryVm>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync();
        var data = _mapper.Map<List<CategoryVm>>(categories);
        return data;
    }
}
