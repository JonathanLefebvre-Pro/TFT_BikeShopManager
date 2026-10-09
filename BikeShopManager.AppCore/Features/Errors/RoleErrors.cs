namespace BikeShopManager.AppCore.Features.Errors;

public static class RoleError
{
    public static Error NotFound => new Error("RoleError.NotFound", "Role not found !");
    public static Error NotInserted => new Error("RoleError.NotInserted", "Role not inserted !");
    public static Error NotUpdated => new Error("RoleError.NotUpdated", "Role not updated !");
    public static Error Empty => new Error("RoleError.TableIsEmpty", "Role table is empty !");
}
