using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly EcommerceDbContext _context;
    public CategoryRepository(EcommerceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsCategoryNameUniqueAsync(string Name)
    {
        return await _context.Categories.AnyAsync(c => c.Name == Name);
    }
}
