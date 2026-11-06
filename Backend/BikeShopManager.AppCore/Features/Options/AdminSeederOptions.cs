using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.AppCore.Features.Options;

public class AdminSeederOptions
{
    public const string SectionName = "AdminSeeder";

    [Required(AllowEmptyStrings = false)]
    public string Login { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string FirstName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string LastName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public int RoleId { get; set; } = 0;
}
