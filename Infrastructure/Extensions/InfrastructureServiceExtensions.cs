using Amazon;
using Amazon.S3;
using Amazon.SecretsManager;
using Application.AppSettingsConfig;
using Application.Contracts.EventBus;
using Application.Contracts.Infrastructure;
using Application.Contracts.Logging;
using Application.Filters;
using Hangfire;
using Infrastructure.LogginService;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        // adding options from aws secret manager .
        services.AddOptions<EmailSettings>().BindConfiguration(nameof(EmailSettings));
        var setting = services.AddOptions<AmazonS3Settings>().BindConfiguration(nameof(AmazonS3Settings));

        services.AddSingleton<IAmazonS3>(sp =>
        {
            var s3Settings = sp.GetRequiredService<IOptions<AmazonS3Settings>>().Value;
            var config = new AmazonS3Config()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(s3Settings.Region),
            };
            return new AmazonS3Client(config);
        });

        services.AddTransient<IEmailRepository, EmailService>();
        services.AddScoped(typeof(IApplicationLogger<>), typeof(LoggerAdapter<>));
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.AddAWSService<IAmazonSecretsManager>();

        //services.AddSingleton<IVendor, FastRyderService>();
        //services.AddSingleton<IVendor, FezDeliveryService>();
        //services.AddSingleton<IVendorProviderService>(nps => new VendorApiProviderService(new Dictionary<string, Func<IVendor>>()
        //{
        //    {"FastRyderService", () => nps.GetServices<IVendor>().First(o => o.GetType() == typeof(FastRyderService)) },
        //    {"FezDeliveryService", () => nps.GetServices<IVendor>().First(o => o.GetType() == typeof(FezDeliveryService)) }
        //}));
        //services.AddHangfire(options =>
        //options.UseSqlServerStorage(configuration.GetConnectionString("EcommerceDbConnectionString"),
        // new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) }));

        //services.AddHangfireServer();

        return services;
    }

    public static WebApplication AddInfrastructureMiddlewares(this WebApplication app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "ECOMMERCE BACKGROUND JOB",
            Authorization = new[]
            {
                new HangfireAuthorizationFilter("admin")
            }
        });

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var cronSettings = services.GetRequiredService<IOptions<CronExpressions>>().Value;
            var jobs = services.GetRequiredService<IRecurringJobs>();
            RecurringJob.AddOrUpdate("Update order", () => jobs.ExecuteSendMail(null), cronSettings.OrderUpdate);
        }
        catch (Exception ex)
        {
            
            var logger = services.GetRequiredService<ILogger>();
            logger.LogError(ex, "An error occured during migration.");
        }
        return app;
    }
}
 