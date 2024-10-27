using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    public ItemRepository(EcommerceDbContext context) : base(context)
    {
    }
}
