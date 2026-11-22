using System.Data;
using BikeShopManager.AppCore.Features.Options;
using Microsoft.Data.SqlClient;

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
        //Serilog.ILogger logger = Log.ForContext(typeof(ConnectionExtension));
        ConnectionStringsOptions? connectionStringsOptions = builder
            .Configuration.GetSection(ConnectionStringsOptions.SectionName)
            .Get<ConnectionStringsOptions>();
        string connectionString =
            $@"Server={connectionStringsOptions?.Server};Database={connectionStringsOptions?.Database};User Id=sa;Password={connectionStringsOptions?.Sa_Password};Trust Server Certificate=True";

        //logger.Debug(connectionString);
        builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(connectionString));

        return builder;
    }
}
