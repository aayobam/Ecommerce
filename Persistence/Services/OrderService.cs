using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class OrderService : GenericRepository<Order>, IOrderRepository
{
    public OrderService(EcommerceDbContext context) : base(context)
    {

    }
}
