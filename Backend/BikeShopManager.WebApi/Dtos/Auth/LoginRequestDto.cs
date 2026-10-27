using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.WebApi.Dtos.Auth;

public class LoginRequestDto
{
    [Required]
    public string Login { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
