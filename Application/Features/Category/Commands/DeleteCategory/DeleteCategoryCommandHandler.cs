using Application.Contracts.Persistence;
using Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Features.Category.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ILogger<DeleteCategoryCommandHandler> logger, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var instance = await _categoryRepository.GetByIdAsync(request.Id);

        if (instance == null)
        {
            _logger.LogInformation($"{nameof(instance)} - {request.Id} not found | {DateTimeOffset.Now}");
            throw new EcommerceNotFoundException("Category not found", HttpStatusCode.NotFound.ToString());
        }

        await _categoryRepository.DeleteAsync(instance);

        return Unit.Value;
    }
}
