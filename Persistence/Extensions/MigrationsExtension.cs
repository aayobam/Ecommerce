using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.DatabaseContexts;

namespace Persistence.Extensions;

public static class MigrationsExtension
{
    public static void ApplyMigrations(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<EcommerceDbContext>>();
            var pendingMigrations = context.Database.GetPendingMigrations();


            // Check and apply pending migrations
           
            if (pendingMigrations.Any())
            {
                logger.LogInformation("Applying pending migrations...");
                context.Database.Migrate();
                logger.LogInformation("Migrations applied successfully...");
            }
            else
            {
                logger.LogInformation("no pending migrations to apply");
            }
        }
    }
}
