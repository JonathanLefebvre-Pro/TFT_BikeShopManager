namespace BikeShopManager.AppCore.Features.Errors;

public static class StockError<T>
{
    public static Error NotFound =>
        new Error($"Stock{typeof(T).Name}Error.NotFound", $"{typeof(T).Name} not found !");
    public static Error NotInserted =>
        new Error($"Stock{typeof(T).Name}Error.NotInserted", $"{typeof(T).Name} not inserted !");
    public static Error NotUpdated =>
        new Error($"Stock{typeof(T).Name}Error.NotUpdated", $"{typeof(T).Name} not updated !");
    public static Error NotDisabled =>
        new Error($"Stock{typeof(T).Name}Error.NotDisabled", $"{typeof(T).Name} not disabled !");
    public static Error NotEnabled =>
        new Error($"Stock{typeof(T).Name}Error.NotEnabled", $"{typeof(T).Name} not enabled !");
    public static Error NotDeleted =>
        new Error($"Stock{typeof(T).Name}Error.NotDeleted", $"{typeof(T).Name} not deleted !");
    public static Error Empty =>
        new Error($"Stock{typeof(T).Name}Error.TableIsEmpty", $"{typeof(T).Name} table is empty !");
}
