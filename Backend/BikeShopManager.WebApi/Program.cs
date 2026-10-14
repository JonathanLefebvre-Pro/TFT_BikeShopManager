using System.Data;
using BikeShopManager.AppCore.Extensions;
using BikeShopManager.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Scalar.AspNetCore;

string myAllowedLocalOrigins = "MyAllowedLocalOrigins";

string conStringTFT = @"GOS-VDI406\TFTIC";
string conStringJo = @"JONATHAN\DATAVIZ";
string connectionString =
    @"Data Source="
    + conStringTFT
    + @";Initial Catalog=TFT_BikeShopManager_WebApi;Integrated Security=True;Trust Server Certificate=True";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: myAllowedLocalOrigins,
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:4200").AllowAnyHeader().AllowAnyMethod();
            policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

// Add services to the container.
builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(connectionString));

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

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
app.UseCors(myAllowedLocalOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();
