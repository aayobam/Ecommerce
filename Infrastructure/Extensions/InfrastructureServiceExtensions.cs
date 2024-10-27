using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceProvider AddInfrastructureService(this IServiceProvider service)
    {
        return service;
    }

    public static WebApplication AddAppBuilder(WebApplication app)
    {
        return app;
    }
}
