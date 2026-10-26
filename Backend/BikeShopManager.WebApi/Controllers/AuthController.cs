using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Queries;
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

            return this.FromResult(result);
        }

        [Route("RefreshAsync")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            Result<TokenResponse> result = await Dispatcher.SendQueryAsync<
                RefreshTokenQuery,
                TokenResponse
            >(new RefreshTokenQuery(refreshToken));

            if (result.IsFailure)
                return this.Unauthorized(new { message = "Invalid refresh token" });

            return this.FromResult(result);
        }

        [Route("LogoutAsync")]
        [HttpPost]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            Result result = await Dispatcher.SendCommandAsync(new LogoutUserCommand(refreshToken));

            if (result.IsFailure)
                return this.Unauthorized(new { message = "Invalid refresh token" });

            return this.FromResult(result);
        }

        //TODO : Register ?
    }
}
