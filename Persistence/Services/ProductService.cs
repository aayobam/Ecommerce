using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class ProductService : GenericRepository<Product>, IProductRepository
{
    public ProductService(EcommerceDbContext context) : base(context)
    {

    }

    public async Task<bool> IsCategoryNameUniqueAsync(string Name)
    {
        var instance = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Name == Name);

        if (instance != null)

            return true;

        return false;
    }
}
