using Api.HealthChecks;
using Api.SignalR;
using Application.AppSettingsConfig;
using Application.Contracts.Infrastructure;
using Application.Filters;
using Domain.Entities;
using Infrastructure.HealthChecks;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Persistence.DatabaseContexts;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

namespace Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EcommerceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(nameof(EcommerceDbContext)));
        });
        services.Configure<OtpSettings>(configuration.GetSection(nameof(OtpSettings)));
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        var maxFailedAccessAttempts = int.Parse(configuration["MaxFailedAccessAttempts"]!);
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddSignalR();

        // Register SignalR Service
        services.AddSingleton<ISignalRService, SignalRService>();

        // Identity configuration.
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedAccount = true;
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.MaxFailedAccessAttempts = maxFailedAccessAttempts;
            options.Lockout.DefaultLockoutTimeSpan = DateTime.Now.AddYears(1) - DateTimeOffset.UtcNow;
        })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<EcommerceDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<SignInManager<ApplicationUser>>();

        //services.AddAuthentication();
        //services.AddAuthorization();
        //services.AddIdentityCore<EcommerceDbContext>();

        // Jwt configuration.
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!))
            };
        });

        services.AddMvc(options =>
        {
            options.Filters.Add(typeof(HttpGlobalExceptionFilter));
        });

        services.AddResponseCompression(option =>
        {
            option.EnableForHttps = true;
        });

        services.AddHttpClient("").ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; }
        }).SetHandlerLifetime(TimeSpan.FromMinutes(5));
        //services.AddHttpContextAccessor();
        //services.AddHttpClient();
        services.AddMemoryCache();

        services.AddResponseCaching(options =>
        {
            options.MaximumBodySize = 8192;
            options.UseCaseSensitivePaths = true;
        });

        return services;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
        });
        return services;
    }

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            };

            options.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirements = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    new string[] { }
                }
            };

            options.AddSecurityRequirement(securityRequirements);

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Door 2 Door APIs",
                Version = "v1",
                Description = "This is the directory of the door to door apis for card instant issuance service",
                Contact = new OpenApiContact
                {
                    Name = "Door 2 Door Api.",
                    Url = new Uri("https://sidd-instant-issuance.com")
                }
            });
        });

        services.AddHealthChecks()
            .AddCheck<CustomHealthCheck>("Custom Health Check", failureStatus: HealthStatus.Degraded, tags: new[] { "custom" });

        services.AddMemoryCache();

        services.AddRateLimiter(limiter =>
        {
            limiter.AddFixedWindowLimiter(policyName: "fixed", options =>
            {
                options.PermitLimit = 5;
                options.Window = TimeSpan.FromMinutes(1);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 2;
            });
        });

        return services;
    }

    public static WebApplication AddApplicationBuilder(WebApplication app)
    {
        app.UseResponseCaching();

        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("Cache-control", "no-store");
            context.Response.Headers.Add("Pragma", "no-cache");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer-when-downgrade");
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000;includeSubDomains;");
            context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
            context.Response.Headers.Add("Content-Security-Policy", "unsafe-inline 'self'");
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
            context.Response.Headers.Add("Feature-Policy", "accelerometer 'none'; camera 'none'; geolocation 'none'; gyroscope 'none'; magnetometer 'none'; microphone 'none';");
            context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue() { MaxAge = TimeSpan.FromSeconds(86000), Public = true };
            await next();
        });

        app.UseHttpsRedirection();

        app.MapHealthChecks("/healthcheck", new HealthCheckOptions
        {
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
            },
            ResponseWriter = JsonWriteResponse.WriteResponse
        }).AllowAnonymous();

        app.MapHub<ChatHub>("/chatHub");

        //app.MapIdentityApi<ApplicationUser>();

        return app;
    }
}
