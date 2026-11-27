using BikeShopManager.AppCore.Extensions.DependencyInjection;
using BikeShopManager.Infrastructure.Extensions;
using BikeShopManager.WebApi.Extensions;
using BikeShopManager.WebApi.Extensions.Connection;
using BikeShopManager.WebApi.Extensions.Cors;
using BikeShopManager.WebApi.Extensions.Jwt;
using BikeShopManager.WebApi.Extensions.Seeding;
using FluentValidation;
using MicroElements.AspNetCore.OpenApi.FluentValidation;
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

    //Change this (in development) to use the Vault (the vault container have to be started and unsealed !!!)
    //False => use jwt and connection secrets from appSettin
    // gs.development.json sections as IOptions<JwtOptions> or IOptions<ConnectionOptions>
    //True => use secrets from Vault
    //bool useVault = !builder.Environment.IsDevelopment();

    builder.Services.AddSerilog(
        (services, lc) =>
            lc.ReadFrom.Configuration(builder.Configuration).ReadFrom.Services(services)
    );

    //Add Generic Mappers
    builder.Services.AddMapperServices();

    //Add CORS config
    builder.Services.AddCorsConfiguration();

    //Add data seeding options (Seed default admin user)
    builder.Services.AddSeedingOptions();

    //Build and add Vault config
    builder.BuildVaultConfiguration();

    //Add jwt and db connection options used in dev mode
    builder.Services.AddJwtOptions();
    builder.Services.AddConnectionOptions();

    //Build and add Db Connection config
    await builder.BuildConnectionConfiguration();

    //Add AppCore and Infra services
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices();

    //Build and add Jwt config
    await builder.BuildJwtConfiguration();

    //Add data seeding hosted services
    builder.Services.AddSeedingServices();

    builder.Services.AddControllers();

    // Register FluentValidation validators
    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

    // Register FluentValidation rules for OpenAPI
    builder.Services.AddFluentValidationRulesToOpenApi();

    // Add OpenApi with FluentValidation transformer
    builder.Services.AddOpenApi(options =>
    {
        options.AddFluentValidationRules();
        options.AddOpenApiTagGroups();
    });

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
