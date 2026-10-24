using System;

namespace BikeShopManager.Domain.Entities;

public enum TokenType
{
    Bearer,
}

public class TokenResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public DateTime ExpireAt { get; set; }
    public TokenType Type { get; set; } = TokenType.Bearer;
}
