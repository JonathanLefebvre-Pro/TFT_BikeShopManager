namespace BikeShopManager.Domain.Entities;

public class JwtRefreshToken
{
    public required string Token { get; init; }
    public required DateTime ExpireAt { get; init; }
}
