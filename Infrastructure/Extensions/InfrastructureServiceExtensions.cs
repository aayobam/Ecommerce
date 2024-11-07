using Application.AppSettingsConfig;
using Application.Contracts.Email;
using Application.Contracts.Logging;
using Application.Contracts.Persistence;
using Infrastructure.EmailService;
using Infrastructure.LogginService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IApplicationLogger<>), typeof(LoggerAdapter<>));
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        return services;
    } 
}
 