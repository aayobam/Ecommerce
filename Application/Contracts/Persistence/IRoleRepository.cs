using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IRoleRepository<T>: IGenericRepository<T> where T : class
{
    Task<bool> IsRoleNameUniqueAsync(string name);
}
