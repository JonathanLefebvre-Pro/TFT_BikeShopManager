namespace BikeShopManager.AppCore.Features.Errors;

public static class CustomerError
{
    public static Error NotFound => new Error("CustomerError.NotFound", "Customer not found !");
    public static Error NotInserted =>
        new Error("CustomerError.NotInserted", "Customer not inserted !");
    public static Error NotUpdated =>
        new Error("CustomerError.NotUpdated", "Customer not updated !");
    public static Error NotDisabled =>
        new Error("CustomerError.NotDisabled", "Customer not disabled !");
    public static Error NotEnabled =>
        new Error("CustomerError.NotEnabled", "Customer not enabled !");
    public static Error TableIsEmpty =>
        new Error("CustomerError.TableIsEmpty", "Customer table is empty !");
}
