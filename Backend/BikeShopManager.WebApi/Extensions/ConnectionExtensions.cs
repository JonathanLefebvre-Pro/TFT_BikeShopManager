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

    public static WebApplicationBuilder BuildConnectionConfiguration(
        this WebApplicationBuilder builder
    )
    {
        ConnectionStringsOptions? connectionStringsOptions = builder
            .Configuration.GetSection(ConnectionStringsOptions.SectionName)
            .Get<ConnectionStringsOptions>();

        builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(
            connectionStringsOptions?.DockerConnection //.DefaultConnection
        ));

        return builder;
    }
}
