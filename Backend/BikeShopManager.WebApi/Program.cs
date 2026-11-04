using BikeShopManager.AppCore.Extensions;
using BikeShopManager.Infrastructure.Extensions;
using BikeShopManager.WebApi.Extensions;
using BikeShopManager.WebApi.Seeders;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsConfiguration();

builder.Services.AddAdminSeederOptions();
builder.Services.AddJwtOptions();
builder.Services.AddConnectionStringsOptions();

builder.AddConnectionStringConfiguration();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.AddJwtConfiguration();
builder.Services.AddHostedService<AdminSeederHostedService>();

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
