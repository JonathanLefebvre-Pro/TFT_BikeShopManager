using System.Data;
using BikeShopManager.AppCore.Extensions;
using BikeShopManager.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Scalar.AspNetCore;

string myAllowedLocalOrigin = "MyAllowedLocalOrigin";

string conStringTFT = @"GOS-VDI406\TFTIC";
string conStringJo = @"JONATHAN\DATAVIZ";
string connectionString =
    @"Data Source="
    + conStringJo
    + @";Initial Catalog=TFT_BikeShopManager_WebApi;Integrated Security=True;Trust Server Certificate=True";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: myAllowedLocalOrigin,
        policy =>
        {
            policy.WithOrigins("https://localhost:4200/");
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
app.UseCors(myAllowedLocalOrigin);
app.UseAuthorization();
app.MapControllers();
app.Run();
