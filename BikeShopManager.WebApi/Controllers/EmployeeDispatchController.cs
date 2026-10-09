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
    public class EmployeeDispatchController(IDispatcher Dispatcher) : ControllerBase
    {
        [Route("GetAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Result<Employee> result = await Dispatcher.SendQueryAsync<
                GetEmployeeByIdQuery,
                Employee
            >(new GetEmployeeByIdQuery(id));
            return this.FromResult(result);
        }

        [Route("UpdateAsync/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateEmployeeDto dto)
        {
            Result result = await Dispatcher.SendCommandAsync<UpdateEmployeeCommand>(
                new UpdateEmployeeCommand(id, dto.User.ToEntity(), dto.Employee.ToEntity())
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
        public async Task<IActionResult> AddAsync([FromBody] AddEmployeeDto Employee)
        {
            Result<int> result = await Dispatcher.SendCommandAsync<AddEmployeeCommand, int>(
                new AddEmployeeCommand(Employee.User.ToEntity(), Employee.Employee.ToEntity())
            );

            return this.FromResult(result);
        }
    }
}
