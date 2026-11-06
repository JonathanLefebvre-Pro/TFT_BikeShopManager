using BikeShopManager.Domain.Entities.Auth;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<bool> RevokeAllActiveTokensAsync(int employeeId);

    Task<bool> RevokeRefreshTokenAsync(string refreshTokenHash);

    Task<RefreshToken?> GetRefreshTokenAsync(string refreshTokenHash);

    Task<int> CreateRefreshToken(RefreshToken refreshToken);

    Task<bool> ReplaceTokenHash(string oldTokenHash, string newTokenHash);
}
