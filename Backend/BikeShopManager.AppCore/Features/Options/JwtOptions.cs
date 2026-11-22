using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.AppCore.Features.Options;

public class JwtOptions
{
    public const string SectionName = "JwtSettings";

    [Required(AllowEmptyStrings = false)]
    public string Secret { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Issuer { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Audience { get; set; } = string.Empty;

    [Range(1, 15)]
    public int ExpirationInMinutes { get; set; }

    [Range(1, 7)]
    public int RefreshTokenDays { get; set; }
}
