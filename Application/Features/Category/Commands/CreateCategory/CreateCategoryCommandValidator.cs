using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(70).WithMessage("{PropertyName} cannot me more than 70 characters");

        RuleFor(x => x)
            .MustAsync(CategoryNameIsUnique).WithMessage("{PropertyName} already exist");
           
    }

    private async Task<bool> CategoryNameIsUnique(CreateCategoryCommand command, CancellationToken token)
    {
        return await _categoryRepository.IsCategoryNameUniqueAsync(command.Name);
    }
}
