using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.AppCore.Features.Options;

public class ConnectionStringsOptions
{
    public const string SectionName = "ConnectionStrings";

    [Required(AllowEmptyStrings = false)]
    public string Server { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Database { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Sa_Password { get; set; } = string.Empty;
}
