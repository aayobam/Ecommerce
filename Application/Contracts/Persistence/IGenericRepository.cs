using Domain.Common;
using System.Linq.Expressions;

namespace Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(List<T> entities); 
    Task UpdateRangeAsync(List<T> entities);
    Task AddRangeAsync(IEnumerable<T> entities);
}
