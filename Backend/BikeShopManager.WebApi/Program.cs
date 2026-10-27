using System.Data;
using BikeShopManager.AppCore.Extensions;
using BikeShopManager.Infrastructure.Extensions;
using BikeShopManager.WebApi.Extensions;
using Microsoft.Data.SqlClient;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder
    .Configuration.GetSection("ConnectionStrings")
    //.GetValue<string>("DefaultConnection")!;
    .GetValue<string>("TechnofuturConnection")!;

builder.Services.ConfigureCors();
builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(connectionString));
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.AddJwtConfiguration();

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
