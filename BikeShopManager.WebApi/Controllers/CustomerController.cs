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
    public class CustomerController(IDispatcher Dispatcher) : ControllerBase
    {
        [Route("GetAsync")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(int id)
        {
            Result<IEnumerable<Customer>> result = await Dispatcher.SendQueryAsync<
                GetCustomersQuery,
                IEnumerable<Customer>
            >(new GetCustomersQuery());
            return this.FromResult(result);
        }

        [Route("GetAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Result<Customer> result = await Dispatcher.SendQueryAsync<
                GetCustomerByIdQuery,
                Customer
            >(new GetCustomerByIdQuery(id));
            return this.FromResult(result);
        }

        [Route("UpdateAsync/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCustomerDto dto)
        {
            Result result = await Dispatcher.SendCommandAsync<UpdateCustomerCommand>(
                new UpdateCustomerCommand(id, dto.User.ToEntity(), dto.Customer.ToEntity())
            );
            return this.FromResult(result);
        }

        [Route("DisableAsync/{id}")]
        [HttpPatch]
        public async Task<IActionResult> DisableAsync(int id)
        {
            Result result = await Dispatcher.SendCommandAsync<DisableCustomerCommand>(
                new DisableCustomerCommand(id)
            );
            return this.FromResult(result);
        }

        [Route("EnableAsync/{id}")]
        [HttpPatch]
        public async Task<IActionResult> EnableAsync(int id)
        {
            Result result = await Dispatcher.SendCommandAsync<EnableCustomerCommand>(
                new EnableCustomerCommand(id)
            );
            return this.FromResult(result);
        }

        [Route("AddAsync")]
        [HttpPost]
        public async Task<IActionResult> AddAsyncDispatcher([FromBody] AddCustomerDto customer)
        {
            Result<int> result = await Dispatcher.SendCommandAsync<AddCustomerCommand, int>(
                new AddCustomerCommand(customer.User.ToEntity(), customer.Customer.ToEntity())
            );

            return this.FromResult(result);
        }
    }
}
