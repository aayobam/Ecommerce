namespace Domain.Abstractions;

public interface IGenericRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<List<T>> GetAllAsync(T entity, string url);
    Task<T> GetByIdAsync(Guid id);
    Task<T> UpdateAsync(Guid id, T entity);
    Task<T> DeleteAsync(Guid id);
}
