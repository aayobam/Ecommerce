using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class ItemService : GenericRepository<Item>, IItemRepository
{
    public ItemService(EcommerceDbContext context) : base(context)
    {
        
    }
}
