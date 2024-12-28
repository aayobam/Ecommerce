using Amazon;
using Amazon.S3;
using Application.AppSettingsConfig;
using Application.Contracts.Infrastructure;
using Application.Contracts.Logging;
using Infrastructure.LogginService;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAmazonS3>(sp =>
        {
            var s3Settings = sp.GetRequiredService<IOptions<AmazonS3Settings>>().Value;
            var config = new AmazonS3Config()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(s3Settings.Region),
            };
            return new AmazonS3Client(config);
        });
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailRepository, EmailService>();
        services.AddScoped(typeof(IApplicationLogger<>), typeof(LoggerAdapter<>));
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        return services;
    }
}
 