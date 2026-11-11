namespace BikeShopManager.Domain.Entities;

public class AuthUser
{
    public required int Id { get; init; }
    public required Role Role { get; init; }
    public required JwtAccessToken AccessToken { get; init; }
    public required JwtRefreshToken RefreshToken { get; init; }
}
