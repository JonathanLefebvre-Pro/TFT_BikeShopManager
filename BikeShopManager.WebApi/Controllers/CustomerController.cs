using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;
using BikeShopManager.WebApi.Infrastructure.Extensions;
using BikeShopManager.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerRepository Repository) : ControllerBase
    {
        [Route("GetAsync/{id}")]
        [HttpGet()]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Result<Customer> result = await Repository.HandleAsync(new GetCustomerByIdQuery(id));
            return this.FromResult(result);
        }

        [Route("UpdateAsync/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCustomerDto dto)
        {
            Result result = await Repository.HandleAsync(
                new UpdateCustomerCommand(id, dto.User.ToEntity(), dto.Customer.ToEntity())
            );
            return this.FromResult(result);
        }

        [Route("DisableAsync/{id}")]
        [HttpPatch]
        public async Task<IActionResult> DisableAsync(int id)
        {
            Result result = await Repository.HandleAsync(new DisableCustomerCommand(id));
            return this.FromResult(result);
        }

        [Route("AddAsync")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddCustomerDto customer)
        {
            Result<int> result = await Repository.HandleAsync(
                new AddCustomerCommand(customer.User.ToEntity(), customer.Customer.ToEntity())
            );
            return this.FromResult(result);
        }
    }
}
