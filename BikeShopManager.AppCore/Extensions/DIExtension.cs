using BikeShopManager.AppCore.Features.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.AppCore.Extensions;

public static class DIExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        return services;
    }
}
