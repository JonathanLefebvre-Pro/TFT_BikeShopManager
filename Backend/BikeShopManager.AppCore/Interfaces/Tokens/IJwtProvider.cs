using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Tokens;

public interface IJwtProvider
{
    Task<JwtAccessToken> GenerateAccessToken(Employee employee);
    Task<JwtRefreshToken> GenerateRefreshToken();
    string HashRefreshToken(string token);
}
