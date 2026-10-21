using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IUserRepository
{
    Task<ExistingUser?> GetUserByMail(string email);
    Task<IEnumerable<User>> GetUsers(string keyword);
    Task<IEnumerable<User>> GetUsers(int offset, int limit, string[] keywords);
    Task<int> GetUsersCount();
    Task<int> GetUsersCount(string[] keywords);
    Task<User?> GetUserById(int id);
    Task<int> CreateUser(User user);
    Task<bool> UpdateUser(int id, User user);
    Task<bool> IsUserExist(int id);
}
