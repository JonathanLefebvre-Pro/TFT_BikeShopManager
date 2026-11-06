using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Tokens;

public interface IJwtProvider
{
    TokenResponse GenerateToken(Employee employee);
    string GenerateRefreshToken();
    string HashRefreshToken(string token);
}
