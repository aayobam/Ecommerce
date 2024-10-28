using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(EcommerceDbContext context) : base(context)
    {
    }

    public Task<bool> IsCategoryNameUniqueAsync(string Name)
    {
        throw new NotImplementedException();
    }
}
