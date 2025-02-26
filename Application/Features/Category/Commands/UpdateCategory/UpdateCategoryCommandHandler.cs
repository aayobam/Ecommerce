using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using Application.DTOs.Category;
using Application.Exceptions;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryVm>
{
    private readonly IApplicationLogger<UpdateCategoryCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(IApplicationLogger<UpdateCategoryCommandHandler> logger,IMapper mapper, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryVm> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCategoryCommandValidator(_categoryRepository);
        var validationResults = await validator.ValidateAsync(request, cancellationToken);

        if (validationResults.Errors.Any())
        {
            _logger.LogInformation($"Validation errors in update request for {nameof(request)} - {request.Id}");
            throw new EcommerceBadRequestException($"Invalid category - {validationResults}", HttpStatusCode.BadRequest.ToString());
        } 
        var model = _mapper.Map<Domain.Entities.Category>(request);
        await _categoryRepository.UpdateAsync(model);
        var instance = _mapper.Map<CategoryVm>(model);
        return instance;
    }
}
