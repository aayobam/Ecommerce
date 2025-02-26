using Application.DTOs.Order;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IOrderRepository: IGenericRepository<Order>
{
}
