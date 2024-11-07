using Application.DTOs.Category;
using MediatR;

namespace Application.Features.Category.Queries.GetCatetoryQuery;

public record GetCategoryDetailsQuery(Guid Id) : IRequest<CategoryVm>;
