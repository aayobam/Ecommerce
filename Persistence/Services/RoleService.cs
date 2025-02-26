using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistence.DatabaseContexts;

namespace Persistence.Repositories;

public class RoleService<T> : IRoleRepository<T> where T : ApplicationRole
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly EcommerceDbContext _context;

    public RoleService(RoleManager<ApplicationRole> roleManager, EcommerceDbContext context)
    {
        _roleManager = roleManager;
        _context = context;
    }

    public Task AddRangeAsync(IEnumerable<T> entities)
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

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(List<T> entities)
    {
        throw new NotImplementedException();
    }
}
