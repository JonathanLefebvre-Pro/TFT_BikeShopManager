using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class AuthRepository(IDbSession DbSession) : IAuthRepository
{
    public Task<Employee?> AuthenticateUser(string login, string password)
    {
        return null;
    }
}
