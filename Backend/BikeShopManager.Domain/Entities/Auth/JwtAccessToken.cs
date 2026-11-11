namespace BikeShopManager.Domain.Entities;

public enum TokenType
{
    Bearer,
}

public class JwtAccessToken
{
    public required string Token { get; init; }
    public required DateTime ExpireAt { get; init; }
    public TokenType Type { get; init; } = TokenType.Bearer;
}
