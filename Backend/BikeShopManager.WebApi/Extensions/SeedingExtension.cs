using BikeShopManager.AppCore.Features.Options;
using BikeShopManager.WebApi.Seeders;

namespace BikeShopManager.WebApi.Extensions.Seeding;

public static class SeedingExtension
{
    public static IServiceCollection AddSeedingOptions(this IServiceCollection services)
    {
        services
            .AddOptions<AdminSeederOptions>()
            .BindConfiguration(AdminSeederOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddSeedingServices(this IServiceCollection services)
    {
        services.AddHostedService<AdminSeederHostedService>();

        return services;
    }
}
