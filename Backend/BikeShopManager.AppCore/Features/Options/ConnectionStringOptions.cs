using System.ComponentModel.DataAnnotations;

namespace BikeShopManager.AppCore.Features.Options;

public class ConnectionStringsOptions
{
    public const string SectionName = "ConnectionStrings";

    [Required(AllowEmptyStrings = false)]
    public string DefaultConnection { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string TechnofuturConnection { get; set; } = string.Empty;
}
