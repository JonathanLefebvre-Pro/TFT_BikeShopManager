using BikeShopManager.AppCore.Extensions.DependencyInjection;
using BikeShopManager.Infrastructure.Extensions;
using BikeShopManager.WebApi.Extensions;
using BikeShopManager.WebApi.Extensions.Connection;
using BikeShopManager.WebApi.Extensions.Cors;
using BikeShopManager.WebApi.Extensions.Jwt;
using BikeShopManager.WebApi.Extensions.Seeding;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting server.");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog(
        (services, lc) =>
            lc.ReadFrom.Configuration(builder.Configuration).ReadFrom.Services(services)
    );

    builder.Services.AddCorsConfiguration();

    builder.Services.AddSeedingOptions();
    // builder.Services.AddJwtOptions();
    // builder.Services.AddConnectionOptions();

    builder.BuildVaultConfiguration();
    await builder.BuildConnectionConfiguration();
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices();
    await builder.BuildJwtConfiguration();
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

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseCors(CorsExtension.AllowedLocalOrigins);
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Server terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}
