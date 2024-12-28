using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.DatabaseContexts;

namespace Persistence.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedData<TEntity>(this IHost host, List<TEntity> entities) where TEntity : Domain.Common.BaseEntity
    {
        var serviceProvider = host.Services.CreateScope().ServiceProvider;
        var context = serviceProvider.GetRequiredService<EcommerceDbContext>();

        var contextEntities = context.Set<TEntity>();
        if (!contextEntities.Any())
        {
            await contextEntities.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedRoles(this IHost host)
    {
        var serviceProvider = host.Services.CreateScope().ServiceProvider;
        var context = serviceProvider.GetRequiredService<EcommerceDbContext>();
        
        List<ApplicationRole> roleList = new List<ApplicationRole>()
        {
            new ApplicationRole(){Name = "admin", NormalizedName = "ADMIN"},
            new ApplicationRole(){Name = "driver", NormalizedName = "DRIVER"},
            new ApplicationRole(){Name = "logistic admin", NormalizedName = "LOGISTIC ADMIN"},
            new ApplicationRole(){Name = "super admin", NormalizedName = "SUPER ADMIN"},
        };

        var existingRole = await context.Roles.Select(x => x.Name).ToListAsync();

        foreach (var role in roleList)
        {
            if (!existingRole.Contains(role.Name))
            {
                context.Roles.Add(role);
            }
        }
        await context.SaveChangesAsync();
    }
}