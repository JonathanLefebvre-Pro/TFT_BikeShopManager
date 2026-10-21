using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IUserRepository
{
    Task<ExistingUser?> GetUserByMail(string email);
    Task<IEnumerable<User>> GetUsersByKeyword(string keyword);
    Task<IEnumerable<User>> GetUsersByKeywords(int offset, int limit, string[] keywords);
    Task<int> GetUsersCountByKeywords(string[] keywords);
    Task<User?> GetUserById(int id);
    Task<int> CreateUser(User user);
    Task<bool> UpdateUser(int id, User user);
    Task<bool> IsUserExist(int id);
}
