using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<Employee?> AuthenticateUser(string login, string password);
}
