using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> IsCategoryNameUniqueAsync(string name);
}
