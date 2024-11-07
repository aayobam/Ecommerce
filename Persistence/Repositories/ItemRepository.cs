using Application.Contracts.Persistence;
using Application.DTOs.Item;
using Domain.Common;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    public ItemRepository(EcommerceDbContext context) : base(context)
    {
    }
}
