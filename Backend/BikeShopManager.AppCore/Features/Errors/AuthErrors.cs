namespace BikeShopManager.AppCore.Features.Errors;

public static class AuthError
{
    public static Error NotAuthorized =>
        new Error("AuthError.NotAuthorized", "You are not authorized to access this endpoint !");

    public static Error NotValidToken =>
        new Error("AuthError.NotValidToken", "The token is not valid !");
}
