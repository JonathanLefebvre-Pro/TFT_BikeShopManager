using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.AppCore.Interfaces.Tokens;
using BikeShopManager.Infrastructure.Services.Repositories;
using BikeShopManager.Infrastructure.Services.Tokens;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.Infrastructure.Extensions;

public static class DIExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
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
