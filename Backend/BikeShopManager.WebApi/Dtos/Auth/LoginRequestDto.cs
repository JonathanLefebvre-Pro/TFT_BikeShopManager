namespace BikeShopManager.WebApi.Dtos.Auth;

public class LoginRequestDto
{
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
}
