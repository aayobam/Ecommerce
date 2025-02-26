using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(EcommerceDbContext context) : base(context)
    {
        
    }

    public async Task<bool> IsCategoryNameUniqueAsync(string Name)
    {
        return await _context.Categories.AnyAsync(c => c.Name == Name);
    }
}
