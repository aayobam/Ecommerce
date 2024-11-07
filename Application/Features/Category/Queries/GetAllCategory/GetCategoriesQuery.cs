using Application.DTOs.Category;
using MediatR;

namespace Application.Features.Category.Queries.GetAllCategory;

public record GetCategoriesQuery : IRequest<List<CategoryVm>>;
