using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.DatabaseContexts;

namespace Persistence.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedRoles(this IHost host)
    {
        var scope = host.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
        
        List<ApplicationRole> roleList = new List<ApplicationRole>()
        {
            new ApplicationRole(){Name = "super admin", NormalizedName = "SUPER ADMIN"},
            new ApplicationRole(){Name = "admin", NormalizedName = "ADMIN"},
            new ApplicationRole(){Name = "driver", NormalizedName = "DRIVER"},
            new ApplicationRole(){Name = "customer", NormalizedName = "CUSTOMER"},
            new ApplicationRole(){Name = "vendor", NormalizedName = "VENDOR"}
        };

        var rolesToAdd = new List<ApplicationRole>();

        var existingRole = await context.Roles
            .AsNoTracking()
            .Select(x => x.Name)
            .ToListAsync();

        foreach (var role in roleList)
        {
            if (!existingRole.Contains(role.Name))
            {
                rolesToAdd.Add(role);
            }
        }
        await context.AddRangeAsync(rolesToAdd);
        await context.SaveChangesAsync();
    }
}