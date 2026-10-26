using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Infrastructure.Services.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.Infrastructure.Extensions;

public static class DIExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDbSession, DbSession>();
        services.Scan(scan =>
            scan.FromAssemblies(typeof(DIExtension).Assembly)
                .AddClasses(classes =>
                    classes.AssignableToAny(
                        typeof(ICommandHandlerAsync<>),
                        typeof(ICommandHandlerAsync<,>),
                        typeof(IQueryHandlerAsync<,>)
                    )
                )
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmployeeRoleRepository, EmployeeRoleRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}
