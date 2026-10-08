namespace BikeShopManager.AppCore.Features.Errors;

public static class CustomerError
{
    public static Error NotFound => new Error("CustomerError.NotFound", "No customer found !");
    public static Error NotInserted =>
        new Error("CustomerError.NotInserted", "Customer was not inserted !");
    public static Error NotUpdated =>
        new Error("CustomerError.NotUpdated", "Customer was not updated !");
    public static Error NotDisabled =>
        new Error("CustomerError.NotDisabled", "Customer was not disabled !");
    public static Error TableIsEmpty =>
        new Error("CustomerError.TableIsEmpty", "Customer table is empty !");
}
