using BikeShopManager.AppCore.Features.Queries.Roles;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;
using BikeShopManager.WebApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ("Admin, Manager"))]
    public class RoleController(IDispatcher Dispatcher) : ControllerBase
    {
        [Route("GetAsync")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            Result<IEnumerable<Role>> result = await Dispatcher.SendQueryAsync<
                GetRolesQuery,
                IEnumerable<Role>
            >(new GetRolesQuery());

            if (result.IsSuccess)
            {
                List<RoleResponseDto> dtos = new List<RoleResponseDto>();
                foreach (Role role in result.Data)
                    dtos.Add(role.ToRoleResponseDto());

                return this.Ok(dtos);
            }

            return this.NotFound(result.Error);
        }

        [Route("GetByIdAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Result<Role> result = await Dispatcher.SendQueryAsync<GetRoleByIdQuery, Role>(
                new GetRoleByIdQuery(id)
            );

            if (result.IsSuccess)
                return this.Ok(result.Data.ToRoleResponseDto());

            return this.NotFound(result.Error);
        }

        [Route("GetByEmployeeIdAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByEmployeeIdAsync(int id)
        {
            Result<Role> result = await Dispatcher.SendQueryAsync<GetRoleByEmployeeIdQuery, Role>(
                new GetRoleByEmployeeIdQuery(id)
            );

            if (result.IsSuccess)
                return this.Ok(result.Data.ToRoleResponseDto());

            return this.NotFound(result.Error);
        }
    }
}
