using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IJwtRepository
{
    Task<TokenResponse> GenerateToken( /*Input data like user or employee*/
    );
}
