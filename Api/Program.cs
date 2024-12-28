using Api.Middlewares;
using Application.Extensions;
using Infrastructure.Extensions;
using Persistence.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddPersitenceService(builder.Configuration)
    //.ConfigureIdentity(builder.Configuration)
    .ConfigureJwt(builder.Configuration)
    .AddApplicationService()
    .AddInfrastructureService(builder.Configuration)
    .ConfigureSwagger();
    

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true"));
    //app.ApplyMigrations();
}

app.UseCors("CorsPolicy");

app.UseCustomHeaders();

//app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
