using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Infrastructure.Services.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.Infrastructure.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmployeeRoleRepository, EmployeeRoleRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.Scan(scan =>
            scan.FromAssemblies(typeof(DependencyInjectionExtension).Assembly)
                .AddClasses(classes => classes.AssignableToAny(typeof(IStockRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }
}
