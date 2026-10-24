using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class JwtRepository(IDbSession DbSession) : IJwtRepository
{
    public async Task<TokenResponse> GenerateToken()
    {
        throw new NotImplementedException();
    }
}
