using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<int> CreateCustomer(int userId, string? vatNumber);
    Task<Customer?> GetCustomerById(int id);
    Task<int> GetCustomerUserId(int id);
    Task<bool> DisableCustomer(int id);
    Task<bool> EnableCustomer(int id);
    Task<bool> UpdateCustomer(int id, string? vatNumber);
    Task<bool> IsCustomerExist(int id);
}
