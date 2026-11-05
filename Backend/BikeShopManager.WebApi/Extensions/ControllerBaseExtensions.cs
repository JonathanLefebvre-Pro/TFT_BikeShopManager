using BikeShopManager.AppCore.Features.Results;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Extensions.Controller;

public static class ControllerBaseExtensions
{
    public static IActionResult FromResult(this ControllerBase controller, Result result)
    {
        if (result.IsSuccess)
            return controller.NoContent();

        return controller.NotFound(result.Error);
    }

    public static IActionResult FromResult<TResult>(
        this ControllerBase controller,
        Result<TResult> result
    )
    {
        if (result.IsSuccess)
            return controller.Ok(result.Data);

        return controller.NotFound(result.Error);
    }

    public static void SetRefreshTokenCookie(
        this ControllerBase controller,
        string refreshToken,
        DateTime expireAt
    )
    {
        controller.Response.Cookies.Append(
            "refreshToken",
            refreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expireAt,
                Path = "/api/Auth",
            }
        );
    }

    public static void DeleteRefreshToken(this ControllerBase controller)
    {
        controller.Response.Cookies.Delete(
            "refreshToken",
            new CookieOptions
            {
                Path = "/api/Auth",
                Secure = true,
                SameSite = SameSiteMode.Strict,
            }
        );
    }
}
