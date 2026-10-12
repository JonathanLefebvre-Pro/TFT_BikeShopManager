using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IDispatcher Dispatcher) : ControllerBase
    {
        [Route("GetRolesAsync")]
        [HttpGet]
        public async Task<IActionResult> GetRolesAsync()
        {
            Result<IEnumerable<Role>> result = await Dispatcher.SendQueryAsync<
                GetRolesQuery,
                IEnumerable<Role>
            >(new GetRolesQuery());

            return this.FromResult(result);
        }

        [Route("GetRoleByIdAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRoleByIdAsync(int id)
        {
            Result<Role> result = await Dispatcher.SendQueryAsync<GetRoleByIdQuery, Role>(
                new GetRoleByIdQuery(id)
            );

            return this.FromResult(result);
        }

        [Route("GetRoleByEmployeeIdAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRoleByEmployeeIdAsync(int id)
        {
            Result<Role> result = await Dispatcher.SendQueryAsync<GetRoleByEmployeeIdQuery, Role>(
                new GetRoleByEmployeeIdQuery(id)
            );

            return this.FromResult(result);
        }
    }
}
