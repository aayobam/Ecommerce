using Application.Contracts.Persistence;
using Application.DTOs.Category;
using Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.Category.Queries.GetCatetoryQuery;

public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, CategoryVm>
{
    private readonly ILogger<GetCategoryDetailsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryDetailsQueryHandler(ILogger<GetCategoryDetailsQueryHandler> logger, IMapper mapper, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryVm> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var instance = await _categoryRepository.GetByIdAsync(request.Id);

        if (instance == null)
        {
            _logger.LogInformation($"{nameof(instance)} - {request.Id} not found | {DateTimeOffset.Now}");
            throw new EcommerceNotFoundException("User not found", HttpStatusCode.NotFound.ToString());
        }

        var data = _mapper.Map<CategoryVm>(instance);
        return data;
    }
}
