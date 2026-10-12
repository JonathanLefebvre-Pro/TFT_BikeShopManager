using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetRoles();

    Task<Role?> GetRoleById(int id);

    Task<bool> IsRoleExist(int id);
}
