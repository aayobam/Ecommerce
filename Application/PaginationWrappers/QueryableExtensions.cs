using Microsoft.EntityFrameworkCore;

namespace Application.PaginationWrappers;

public static class QueryableExtensions
{
    public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(IQueryable<T> source, int pageNumber, int pageSize) where T : class
    {        
        int count = await source.AsNoTracking().CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedResult<T>(items, count, pageNumber, pageSize);
    }
}
