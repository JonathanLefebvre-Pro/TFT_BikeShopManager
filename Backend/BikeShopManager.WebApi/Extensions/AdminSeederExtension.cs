using BikeShopManager.AppCore.Features.Options;

namespace BikeShopManager.WebApi.Extensions;

public static class AdminSeederExtension
{
    public static IServiceCollection AddAdminSeederOptions(this IServiceCollection services)
    {
        services
            .AddOptions<AdminSeederOptions>()
            .BindConfiguration(AdminSeederOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
