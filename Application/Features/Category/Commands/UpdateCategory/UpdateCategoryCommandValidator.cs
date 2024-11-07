 using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} cannot me more than 70 characters");

        RuleFor(x => x)
            .MustAsync(CategoryNameIsUnique).WithMessage("{PropertyName} already exist");

        RuleFor(x => x.Id)
            .NotNull()
            .MustAsync(CategoryMustExist);
    }

    private async Task<bool> CategoryMustExist(Guid id, CancellationToken token)
    {
        var instance = await _categoryRepository.GetByIdAsync(id);
        return instance != null;
    }

    private async Task<bool> CategoryNameIsUnique(UpdateCategoryCommand command, CancellationToken token)
    {
        return await _categoryRepository.IsCategoryNameUniqueAsync(command.Name);
    }
}
