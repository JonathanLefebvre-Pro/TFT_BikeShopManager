namespace BikeShopManager.WebApi.Extensions;

public class ValidatorExtensions
{
    public static bool BeValidBirthDate(DateOnly? birthdate)
    {
        return birthdate is null
            || (
                birthdate <= DateOnly.FromDateTime(DateTime.Today)
                && birthdate > new DateOnly(1920, 01, 01)
            );
    }
}
