using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Tokens;

public interface IJwtProvider
{
    JwtAccessToken GenerateAccessToken(Employee employee);
    JwtRefreshToken GenerateRefreshToken();
    string HashRefreshToken(string token);
}
