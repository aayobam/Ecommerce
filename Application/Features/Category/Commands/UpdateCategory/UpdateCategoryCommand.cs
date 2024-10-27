using Application.DTOs.Category;
using MediatR;

namespace Application.Features.Category.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<CategoryVm>
{
    public string Name { get; set; }
}
