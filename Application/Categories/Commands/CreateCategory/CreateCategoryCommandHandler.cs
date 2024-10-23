using Domain.Dtos.Category;
using MediatR;

namespace Application.Categories.Commands.CreateCategory;

internal sealed class CreateCategoryCommandHandler : IRequest<CreateCategoryVm>;
