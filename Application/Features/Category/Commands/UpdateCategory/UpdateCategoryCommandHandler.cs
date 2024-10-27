using Application.Contracts.Persistence;
using Application.DTOs.Category;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryVm>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryVm> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Domain.Entities.Category>(request);
        var instance = await _categoryRepository.UpdateAsync(model);
        var data = _mapper.Map<CategoryVm>(instance);
        return data;
    }
}
