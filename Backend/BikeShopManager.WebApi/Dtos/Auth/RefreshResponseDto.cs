namespace BikeShopManager.WebApi.Dtos.Auth;

public class RefreshResponseDto
{
    public required string AccessToken { get; init; }
    public required DateTime ExpireAt { get; init; }
    public required int UserId { get; init; }
    public required string Role { get; init; }
}
