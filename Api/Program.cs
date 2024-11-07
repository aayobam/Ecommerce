using Application.Extensions;
using Infrastructure.Extensions;
using Persistence.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationService()
    .AddPersitenceService(builder.Configuration)
    .AddInfrastructureService(builder.Configuration)
    .ConfigureSwagger()
    .ConfigureJwt(builder.Configuration)
    .ConfigureIdentity(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    );
}); 
    
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();



//app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHealthChecks("/health", new HealthCheckOptions()
//{
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//});

//app.UseSerilogRequestLogging();

app.UseCors("CorsPolicy");

app.UseCustomHeaders();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
