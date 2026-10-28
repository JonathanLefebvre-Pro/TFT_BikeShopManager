namespace BikeShopManager.Domain.Entities;

public class LoginRequest
{
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
}
