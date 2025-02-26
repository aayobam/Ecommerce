using Application.Contracts.Persistence;
using Application.DTOs.Category;
using Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryVm>
{
    private readonly ILogger<CreateCategoryCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ILogger<CreateCategoryCommandHandler> logger, IMapper mapper, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryVm> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateCategoryCommandValidator(_categoryRepository);
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogInformation($"\n bad request | {DateTimeOffset.Now} \n");
            throw new EcommerceBadRequestException(validationResult, HttpStatusCode.BadRequest.ToString());
        }

        var model = _mapper.Map<Domain.Entities.Category>(request);
        await _categoryRepository.CreateAsync(model);
        var instance = _mapper.Map<CategoryVm>(request);
        return instance;
    }
}
