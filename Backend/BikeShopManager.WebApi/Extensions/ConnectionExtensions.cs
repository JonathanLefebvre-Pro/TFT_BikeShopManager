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
        this WebApplicationBuilder builder,
        bool useVault
    )
    {
        //Serilog.ILogger logger = Log.ForContext(typeof(ConnectionExtension));
        string? connectionString = string.Empty;

        if (useVault)
        {
            IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            IVaultClient vaultClient = serviceProvider.GetRequiredService<IVaultClient>();

            IDictionary<string, object> secrets =
                await AppCore.Extensions.VaultExtensions.GetSecret(
                    vaultClient,
                    "bikeshopmanager/db"
                );

            string? serverSecret = (string)secrets["server"];
            string? databaseSecret = (string)secrets["database"];
            string? saPasswordSecret = (string)secrets["sa_password"];
            connectionString =
                $@"Server={serverSecret};Database={databaseSecret};User Id=sa;Password={saPasswordSecret};Trust Server Certificate=True";
        }
        else
        {
            ConnectionStringsOptions? connectionStringsOptions = builder
                .Configuration.GetSection(ConnectionStringsOptions.SectionName)
                .Get<ConnectionStringsOptions>();

            connectionString = connectionStringsOptions?.DockerConnection;
        }

        //logger.Debug(connectionString);
        builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(connectionString));

        return builder;
    }
}
