using Application.AppSettingsConfig;
using Application.Contracts.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContexts;
using Persistence.Repositories;
using Persistence.Seeders;

namespace Persistence.Extensions;

public static class PersistenceServiceExtension
{
    public static IServiceCollection AddPersitenceService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<OtpSettings>().BindConfiguration(nameof(OtpSettings));
        // Database configuration
        services.AddDbContext<EcommerceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(nameof(EcommerceDbContext)));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IUserRepository<>), typeof(UserService<>));
        services.AddScoped(typeof(IRoleRepository<>), typeof(RoleService<>));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductService>();
        services.AddScoped<IItemRepository, ItemService>();
        services.AddScoped<IReviewRepository, ReviewService>();
        services.AddScoped<IOrderRepository, OrderService>();
        services.AddScoped<IAuthRepository, AuthService>();
        services.AddScoped<IVendorRepository, VendorService>();

        return services;
    }

    public static WebApplication AddDataSeeding(WebApplication app)
    {
        app.SeedRoles().Wait();
        return app;
    }
}
