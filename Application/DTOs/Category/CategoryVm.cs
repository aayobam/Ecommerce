using Domain.Common;

namespace Application.DTOs.Category;

public class CategoryVm : BaseEntity
{
    public required string Name { get; set; }
}
