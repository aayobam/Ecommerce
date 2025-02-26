using Api.Middlewares;
using Application.Extensions;
using Infrastructure.Extensions;
using Persistence.Extensions;
using System.Net;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationService(builder.Configuration)
    .AddPersitenceService(builder.Configuration)
    .AddInfrastructureService(builder.Configuration)
    .ConfigureSwagger(builder.Configuration);

// aws secret manager
//builder.Configuration.AddSecretsManager(configurator: config =>
//{
//    config.SecretFilter = record => record.Name.StartsWith($"{builder.Environment.EnvironmentName}/Demo/");
//    config.KeyGenerator = (secret, name) => name.Replace($"{builder.Environment.EnvironmentName}/Demo/", string.Empty);
//    config.PollingInterval = TimeSpan.FromSeconds(5);
//    //config.KeyGenerator = (secret, name) => name.Replace($"{builder.Environment.EnvironmentName}/Demo/", string.Empty).Replace("__", ":");
//});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapShortCircuit((int)HttpStatusCode.NotFound, "robots.txt", "favicon.ico");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true"));
}

ApplicationServiceExtension.AddApplicationBuilder(app);

//InfrastructureServiceExtensions.AddInfrastructureMiddlewares(app);

MigrationsExtension.ApplyMigrations(app);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("CorsPolicy");

app.Run();