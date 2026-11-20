using BikeShopManager.AppCore.Features.Commands.Auth;
using BikeShopManager.AppCore.Features.Queries.Auth;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos.Auth;
using BikeShopManager.WebApi.Extensions.Controller;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IDispatcher Dispatcher, IValidator<LoginRequestDto> Validator) : ControllerBase
    {
        [Route("LoginAsync")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            FluentValidation.Results.ValidationResult validationResult = await Validator.ValidateAsync(dto);
            if(!validationResult.IsValid)
                return this.ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));

            Result<AuthUser> result = await Dispatcher.SendQueryAsync<LoginQuery, AuthUser>(
                new LoginQuery(dto.Login, dto.Password)
            );

            if (result.IsFailure)
                return this.Unauthorized(new { message = "Invalid username or password" });

            this.SetRefreshTokenCookie(
                result.Data.RefreshToken.Token,
                result.Data.RefreshToken.ExpireAt
            );

            LoginResponseDto response = new LoginResponseDto
            {
                UserId = result.Data.Id,
                Role = result.Data.Role.Name,
                AccessToken = result.Data.AccessToken.Token,
                ExpireAt = result.Data.AccessToken.ExpireAt,
            };

            return Ok(response);
        }

        [Route("RefreshAsync")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken()
        {
            string? refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            Result<AuthUser> result = await Dispatcher.SendQueryAsync<RefreshTokenQuery, AuthUser>(
                new RefreshTokenQuery(refreshToken)
            );

            if (result.IsFailure)
                return this.Unauthorized(new { message = "Invalid refresh token" });

            this.SetRefreshTokenCookie(
                result.Data.RefreshToken.Token,
                result.Data.RefreshToken.ExpireAt
            );

            RefreshResponseDto response = new RefreshResponseDto
            {
                UserId = result.Data.Id,
                Role = result.Data.Role.Name,
                AccessToken = result.Data.AccessToken.Token,
                ExpireAt = result.Data.AccessToken.ExpireAt,
            };

            return Ok(response);
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
    }
}
