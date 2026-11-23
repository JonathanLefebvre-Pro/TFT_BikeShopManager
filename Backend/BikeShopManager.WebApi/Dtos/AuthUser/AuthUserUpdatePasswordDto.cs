namespace BikeShopManager.WebApi.Dtos;

public class AuthUserUpdatePasswordDto
{
    #region Employee
    public required string OldPassword { get; set; }

    public required string NewPassword { get; set; }
    #endregion
}
