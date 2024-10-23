using Domain.Dtos.Category;
using MediatR;

namespace Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommandRequest(CreateCategoryVm request) : IRequest<CreateCategoryVm>;
