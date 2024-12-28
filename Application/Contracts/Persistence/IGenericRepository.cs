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
    Task UpdateRangeAsync(List<T> entity);
    IQueryable<T> QueryAll(params Expression<Func<T, bool>>[] predicates);
    Task AddRangeAsync(IEnumerable<T> entity);
    Task<T> FirstOrDefaultNoTracking(Expression<Func<T, bool>> predicate);
}
