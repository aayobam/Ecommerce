using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServiceExtension).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
