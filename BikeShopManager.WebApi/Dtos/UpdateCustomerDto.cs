namespace BikeShopManager.WebApi.Dtos;

public class UpdateCustomerDto
{
    public UserDto User { get; set; } = default!;

    public CustomerDto Customer { get; set; } = default!;
}
