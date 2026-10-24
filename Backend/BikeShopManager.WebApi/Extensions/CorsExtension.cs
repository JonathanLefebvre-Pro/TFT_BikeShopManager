using System;

namespace BikeShopManager.WebApi.Extensions;

public static class CorsExtension
{
    private const string myAllowedLocalOrigins = "MyAllowedLocalOrigins";
    public static string AllowedLocalOrigins
    {
        get => myAllowedLocalOrigins;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
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

        return services;
    }
}
