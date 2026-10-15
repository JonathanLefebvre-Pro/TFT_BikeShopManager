using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;
using BikeShopManager.WebApi.Infrastructure.Extensions;
using BikeShopManager.WebApi.Mappers;
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

            if (result.IsSuccess)
            {
                List<RoleReturnDto> dtos = new List<RoleReturnDto>();
                foreach (Role role in result.Data)
                    dtos.Add(role.ToRoleReturnDto());

                return this.Ok(dtos);
            }

            return this.NotFound(result.Error);
        }

        [Route("GetRoleByIdAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRoleByIdAsync(int id)
        {
            Result<Role> result = await Dispatcher.SendQueryAsync<GetRoleByIdQuery, Role>(
                new GetRoleByIdQuery(id)
            );

            if (result.IsSuccess)
                return this.Ok(result.Data.ToRoleReturnDto());

            return this.NotFound(result.Error);
        }

        [Route("GetRoleByEmployeeIdAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRoleByEmployeeIdAsync(int id)
        {
            Result<Role> result = await Dispatcher.SendQueryAsync<GetRoleByEmployeeIdQuery, Role>(
                new GetRoleByEmployeeIdQuery(id)
            );

            if (result.IsSuccess)
                return this.Ok(result.Data.ToRoleReturnDto());

            return this.NotFound(result.Error);
        }
    }
}
