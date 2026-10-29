namespace BikeShopManager.WebApi.Dtos.Auth;

public class AccessTokenResponseDto
{
    public string AccessToken { get; set; } = default!;
    public DateTime ExpireAt { get; set; }
}
