using Application.Contracts.Persistence;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{   
    protected readonly EcommerceDbContext _context;

    public GenericRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> AddUsersToRolesAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(List<T> entity)
    {
        _context.Set<T>().UpdateRange(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<T> QueryAll(params Expression<Func<T, bool>>[] predicates)
    {
        var query = _context.Set<T>().OrderByDescending(x => x.DateCreated).AsQueryable();

        query = query.AsNoTracking();

        if (predicates != null && predicates.Length > 0)
        {
            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }
        }
        return query;
    }

    public async Task AddRangeAsync(IEnumerable<T> entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        };
        await _context.Set<T>().AddRangeAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T> FirstOrDefaultNoTracking(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task DeleteRangeAsync(List<T> entities)
    {
        foreach(var entity in entities)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }
        await _context.SaveChangesAsync();
    }
}
