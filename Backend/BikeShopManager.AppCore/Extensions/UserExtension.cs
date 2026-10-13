using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Extensions;

public static class UserExtension
{
    public static bool IsSameIdentity(ExistingUser existingUser, User newUser)
    {
        return string.Equals(existingUser.Email, newUser.Email, StringComparison.OrdinalIgnoreCase)
            && string.Equals(
                existingUser.FirstName,
                newUser.FirstName,
                StringComparison.OrdinalIgnoreCase
            )
            && string.Equals(
                existingUser.LastName,
                newUser.LastName,
                StringComparison.OrdinalIgnoreCase
            )
            && (
                existingUser.BirthDate is null
                || newUser.BirthDate is null
                || existingUser.BirthDate == newUser.BirthDate
            );
    }
}
