namespace BikeShopManager.AppCore.Features.Errors;

public static class UserError
{
    public static Error NotFound => new Error("UserError.NotFound", "User not found !");
    public static Error NotInserted => new Error("UserError.NotInserted", "User not inserted !");
    public static Error NotUpdated => new Error("UserError.NotUpdated", "User not updated !");
    public static Error NotDisabled => new Error("UserError.NotDisabled", "User not disabled !");
    public static Error NotEnabled => new Error("UserError.NotEnabled", "User not enabled !");
    public static Error Empty => new Error("UserError.TableIsEmpty", "User table is empty !");
}
