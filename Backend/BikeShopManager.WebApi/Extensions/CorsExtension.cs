namespace BikeShopManager.WebApi.Extensions.Cors;

public static class CorsExtension
{
    private const string myAllowedLocalOrigins = "MyAllowedLocalOrigins";
    public static string AllowedLocalOrigins
    {
        get => myAllowedLocalOrigins;
    }

    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                name: myAllowedLocalOrigins,
                policy =>
                {
                    policy
                        .WithOrigins(
                            "http://127.0.0.1:4200",
                            "http://localhost:4200",
                            "http://127.0.0.1:8080",
                            "http://localhost:8080",
                            "http://bikeshopmanager-client:8080"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
            );
        });

        return services;
    }
}
