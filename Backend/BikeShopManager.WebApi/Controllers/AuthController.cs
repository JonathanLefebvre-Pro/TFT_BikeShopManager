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
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            Result<TokenResponse> result = await Dispatcher.SendQueryAsync<
                LoginUserQuery,
                TokenResponse
            >(new LoginUserQuery(dto.ToLoginRequestEntity()));

            return this.FromResult(result);
        }

        //TODO : Logout
        //TODO : Register ?
        //TODO : Refresh token
    }
}
