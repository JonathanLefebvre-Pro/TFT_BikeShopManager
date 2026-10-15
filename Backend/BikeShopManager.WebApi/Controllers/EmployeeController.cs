using BikeShopManager.AppCore.Features.Commands;
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
    public class EmployeeController(IDispatcher Dispatcher) : ControllerBase
    {
        [Route("GetAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Result<Employee> result = await Dispatcher.SendQueryAsync<
                GetEmployeeByIdQuery,
                Employee
            >(new GetEmployeeByIdQuery(id));

            if (result.IsSuccess)
                return this.Ok(result.Data.ToEmployeeReturnDto());

            return this.NotFound(result.Error);
        }

        [Route("UpdateAsync/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] EmployeeUpdateDto dto)
        {
            Result result = await Dispatcher.SendCommandAsync<UpdateEmployeeCommand>(
                new UpdateEmployeeCommand(id, dto.ToEmployeeEntity(), dto.ToUserEntity())
            );
            return this.FromResult(result);
        }

        [Route("DisableAsync/{id}")]
        [HttpPatch]
        public async Task<IActionResult> DisableAsync(int id)
        {
            Result result = await Dispatcher.SendCommandAsync<DisableEmployeeCommand>(
                new DisableEmployeeCommand(id)
            );
            return this.FromResult(result);
        }

        [Route("EnableAsync/{id}")]
        [HttpPatch]
        public async Task<IActionResult> EnableAsync(int id)
        {
            Result result = await Dispatcher.SendCommandAsync<EnableEmployeeCommand>(
                new EnableEmployeeCommand(id)
            );
            return this.FromResult(result);
        }

        [Route("AddAsync")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] EmployeeAddDto dto)
        {
            Result<int> result = await Dispatcher.SendCommandAsync<AddEmployeeCommand, int>(
                new AddEmployeeCommand(dto.ToEmployeeEntity(), dto.ToUserEntity(), dto.RoleId)
            );

            return this.FromResult(result);
        }

        [Route("GetEmployeesByRoleIdAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeesByRoleIdAsync(int id)
        {
            Result<IEnumerable<Employee>> result = await Dispatcher.SendQueryAsync<
                GetEmployeesByRoleIdQuery,
                IEnumerable<Employee>
            >(new GetEmployeesByRoleIdQuery(id));

            if (result.IsSuccess)
            {
                List<EmployeeReturnDto> dtos = new List<EmployeeReturnDto>();
                foreach (Employee employee in result.Data)
                    dtos.Add(employee.ToEmployeeReturnDto());

                return this.Ok(dtos);
            }

            return this.NotFound(result.Error);
        }
    }
}
