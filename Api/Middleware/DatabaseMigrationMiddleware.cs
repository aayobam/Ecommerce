using Microsoft.EntityFrameworkCore;
using Polly;
using Persistence.DatabaseContexts;
using Microsoft.Data.SqlClient;

namespace Api.Middleware;

public static class DatabaseMigrationMiddleware
{
    public static void ApplyMigrations(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var logger = services.GetRequiredService<ILogger<EcommerceDbContext>>();
            var db = services.GetRequiredService<EcommerceDbContext>();
            var configuration = services.GetRequiredService<IConfiguration>();

            try
            {
                logger.LogInformation($"Migrating database associated with context {typeof(DatabaseMigrationMiddleware).Name}");

                var retryPolicy = Policy.Handle<SqlException>()
                    .WaitAndRetry(
                    new[]
                    {
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(15),
                    TimeSpan.FromMinutes(20),
                    },
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        logger.LogWarning($"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before next retry.");
                    });

                retryPolicy.Execute(() =>
                {
                    // Ensure database connection and apply migrations
                    if (db.Database.GetPendingMigrations().Any())
                    {
                        db.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
                        db.Database.Migrate();
                        logger.LogInformation("Database migrations applied successfully.");
                    }
                    else
                    {
                        logger.LogInformation("No pending migrations found.");
                    }
                });

                logger.LogInformation($"Migrated database associated with context {typeof(DatabaseMigrationMiddleware).Name}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(DatabaseMigrationMiddleware).Name}");
            }
        }
    }
}