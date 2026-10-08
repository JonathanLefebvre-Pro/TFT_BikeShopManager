namespace BikeShopManager.WebApi.Dtos;

public class AddCustomerDto
{
    public UserDto User { get; set; } = default!;

    public CustomerDto Customer { get; set; } = default!;
}
