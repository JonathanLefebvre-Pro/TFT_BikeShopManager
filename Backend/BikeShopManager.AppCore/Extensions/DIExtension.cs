using BikeShopManager.AppCore.Features.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.AppCore.Extensions;

public static class DIExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<Dispatcher>();
        services.AddScoped<IDispatcher, TransactionalDispatcherDecorator>(s =>
        {
            var dispatcher = s.GetRequiredService<Dispatcher>();
            var dbSession = s.GetRequiredService<IDbSession>();

            return new TransactionalDispatcherDecorator(dispatcher, dbSession);
        });

        return services;
    }
}
