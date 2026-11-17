using System.Data;
using BikeShopManager.AppCore.Features.Options;
using Microsoft.Data.SqlClient;
using VaultSharp;

namespace BikeShopManager.WebApi.Extensions.Connection;

public static class ConnectionExtension
{
    public static IServiceCollection AddConnectionOptions(this IServiceCollection services)
    {
        services
            .AddOptions<ConnectionStringsOptions>()
            .BindConfiguration(ConnectionStringsOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static async Task<WebApplicationBuilder> BuildConnectionConfiguration(
        this WebApplicationBuilder builder
    )
    {
        // ConnectionStringsOptions? connectionStringsOptions = builder
        //     .Configuration.GetSection(ConnectionStringsOptions.SectionName)
        //     .Get<ConnectionStringsOptions>();

        //Serilog.ILogger logger = Log.ForContext(typeof(ConnectionExtension));
        IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
        IVaultClient vaultClient = serviceProvider.GetRequiredService<IVaultClient>();

        IDictionary<string, object> secrets = await AppCore.Extensions.VaultExtensions.GetSecret(
            vaultClient,
            "bikeshopmanager-webapp/db"
        );
        var serverSecret = secrets["server"];
        var databaseSecret = secrets["database"];
        var saPasswordSecret = secrets["sa_password"];
        string connectionString =
            $@"Server={serverSecret};Database={databaseSecret};User Id=sa;Password={saPasswordSecret};Trust Server Certificate=True";
        //logger.Debug(connectionString);

        builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(
            connectionString //connectionStringsOptions?.DockerConnection
        ));

        return builder;
    }
}
