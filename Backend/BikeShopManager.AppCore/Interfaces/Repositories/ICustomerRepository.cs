using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetCustomers(int offset, int limit);
    Task<int> CreateCustomer(int userId, string? vatNumber);
    Task<Customer?> GetCustomerById(int id);
    Task<Customer?> GetCustomerByUserId(int id);
    Task<int> GetCustomerUserId(int id);
    Task<int> GetCustomersCount();
    Task<bool> DisableCustomer(int id);
    Task<bool> EnableCustomer(int id);
    Task<bool> UpdateCustomer(int id, string? vatNumber);
    Task<bool> IsCustomerExist(int id);
}
