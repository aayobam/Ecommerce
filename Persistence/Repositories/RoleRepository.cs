using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class RoleRepository<T> : IRoleRepository<T> where T : ApplicationRole
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleRepository(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public Task AddRangeAsync(IEnumerable<T> entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(List<T> entities)
    {
        throw new NotImplementedException();
    }

    public Task<T> FirstOrDefaultNoTracking(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsRoleNameUniqueAsync(string name)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> QueryAll(params Expression<Func<T, bool>>[] predicates)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(List<T> entity)
    {
        throw new NotImplementedException();
    }
}
