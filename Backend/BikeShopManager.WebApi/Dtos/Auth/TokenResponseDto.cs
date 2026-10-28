using BikeShopManager.Domain.Entities;

namespace BikeShopManager.WebApi.Dtos.Auth;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public DateTime ExpireAt { get; set; }
    public TokenType Type { get; set; } = TokenType.Bearer;
}
