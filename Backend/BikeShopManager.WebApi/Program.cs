using BikeShopManager.AppCore.Extensions.DependencyInjection;
using BikeShopManager.Infrastructure.Extensions;
using BikeShopManager.WebApi.Extensions.Connection;
using BikeShopManager.WebApi.Extensions.Cors;
using BikeShopManager.WebApi.Extensions.Jwt;
using BikeShopManager.WebApi.Extensions.Seeding;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsConfiguration();

builder.Services.AddSeedingOptions();
builder.Services.AddJwtOptions();
builder.Services.AddConnectionOptions();

builder.BuildConnectionConfiguration();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.BuildJwtConfiguration();
builder.Services.AddSeedingServices();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors(CorsExtension.AllowedLocalOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
