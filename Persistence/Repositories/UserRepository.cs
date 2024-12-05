using Application.Contracts.Persistence;
using Application.DTOs.ApplicationUser;
using Application.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class UserRepository<T> : IUserRepository<T> where T : ApplicationUser
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly EcommerceDbContext _context;

    public UserRepository(UserManager<ApplicationUser> userManager, EcommerceDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<T> entity)
    {
        foreach (T entityItem in entity)
        {
            await _userManager.CreateAsync(entityItem);
        }
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _userManager.CreateAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        await _userManager.DeleteAsync(entity);
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

    public async Task<bool> IsEmailAddressUniqueAsync(string emailAddress)
    {
        var instance =  await _userManager.FindByEmailAsync(emailAddress);
        if (instance == null)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber)
    {
        var instance = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        if (instance == null)
        {
            return true;
        }
        return false;
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

    public Task<GenericResponse> VerifyUserAsync(VerifyUserVm request)
    {
        throw new NotImplementedException();
    }
}
