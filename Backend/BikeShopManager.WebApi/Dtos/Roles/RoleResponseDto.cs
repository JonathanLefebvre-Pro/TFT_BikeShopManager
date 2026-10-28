namespace BikeShopManager.WebApi.Dtos;

public class RoleResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string NormalizedName { get; set; }
}
