namespace BikeShopManager.AppCore.Features.Errors;

public static class StockArticleError
{
    public static Error NotFound => new Error("StockArticleError.NotFound", "Article not found !");
    public static Error NotInserted =>
        new Error("StockArticleError.NotInserted", "Article not inserted !");
    public static Error NotUpdated =>
        new Error("StockArticleError.NotUpdated", "Article not updated !");
    public static Error NotDisabled =>
        new Error("StockArticleError.NotDisabled", "Article not disabled !");
    public static Error NotEnabled =>
        new Error("StockArticleError.NotEnabled", "Article not enabled !");
    public static Error Empty =>
        new Error("StockArticleError.TableIsEmpty", "Article table is empty !");
}
