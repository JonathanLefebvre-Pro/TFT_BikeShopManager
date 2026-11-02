using BikeShopManager.AppCore.Features.Commands.Auth;
using BikeShopManager.AppCore.Features.Queries.Auth;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos.Auth;
using BikeShopManager.WebApi.Infrastructure.Extensions;
using BikeShopManager.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IDispatcher Dispatcher) : ControllerBase
    {
        [Route("LoginAsync")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            Result<TokenResponse> result = await Dispatcher.SendQueryAsync<
                LoginUserQuery,
                TokenResponse
            >(new LoginUserQuery(dto.ToLoginRequestEntity()));

            if (result.IsFailure)
                return this.Unauthorized(new { message = "Invalid username or password" });

            this.SetRefreshTokenCookie(result.Data.RefreshToken, result.Data.RefreshExpireAt);

            AccessTokenResponseDto accessToken = new AccessTokenResponseDto
            {
                AccessToken = result.Data.AccessToken,
                ExpireAt = result.Data.AccessExpireAt,
            };

            return Ok(accessToken);
        }

        [Route("RefreshAsync")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken()
        {
            string? refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            Result<TokenResponse> result = await Dispatcher.SendQueryAsync<
                RefreshTokenQuery,
                TokenResponse
            >(new RefreshTokenQuery(refreshToken));

            if (result.IsFailure)
                return this.Unauthorized(new { message = "Invalid refresh token" });

            this.SetRefreshTokenCookie(result.Data.RefreshToken, result.Data.RefreshExpireAt);

            AccessTokenResponseDto accessToken = new AccessTokenResponseDto
            {
                AccessToken = result.Data.AccessToken,
                ExpireAt = result.Data.AccessExpireAt,
            };

            return Ok(accessToken);
        }

        [Route("LogoutAsync")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            string? refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            this.DeleteRefreshToken();

            Result result = await Dispatcher.SendCommandAsync(new LogoutUserCommand(refreshToken));

            if (result.IsFailure)
                return this.Unauthorized(new { message = "Invalid refresh token" });

            return this.FromResult(result);
        }

        //TODO : Register ?
    }
}
