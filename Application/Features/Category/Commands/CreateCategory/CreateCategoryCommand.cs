using Application.DTOs.Category;
using MediatR;

namespace Application.Features.Category.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<CategoryVm>
{
    public string Name { get; set; }
}
