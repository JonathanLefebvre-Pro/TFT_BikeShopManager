using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IJwtProvider
{
    TokenResponse GenerateToken(Employee employee);
    string GenerateRefreshToken();
    string HashToken(string token);
}
