using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Commands.Stock;
using BikeShopManager.AppCore.Features.Queries.Stock;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Extensions.Controller;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public abstract class StockControllerBase<T>(IDispatcher Dispatcher) : ControllerBase
        where T : StockBase
    {
        [Route("GetAsync")]
        [HttpGet]
        //[Authorize(Roles = ("Admin, Manager, Seller, Mechanic"))]
        public virtual async Task<IActionResult> GetAsync()
        {
            Result<IEnumerable<T>> result = await Dispatcher.SendQueryAsync<
                GetStockQuery<T>,
                IEnumerable<T>
            >(new GetStockQuery<T>());

            return this.FromResult(result);
        }

        [Route("GetByIdAsync")]
        [HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(int id)
        {
            Result<T> result = await Dispatcher.SendQueryAsync<GetStockByIdQuery<T>, T>(
                new GetStockByIdQuery<T>(id)
            );

            return this.FromResult(result);
        }

        [Route("GetByNameAsync")]
        [HttpGet]
        public virtual async Task<IActionResult> GetByNameAsync(string name)
        {
            Result<T> result = await Dispatcher.SendQueryAsync<GetStockByNameQuery<T>, T>(
                new GetStockByNameQuery<T>(name)
            );

            return this.FromResult(result);
        }

        [Route("AddAsync")]
        [HttpPost]
        public virtual async Task<IActionResult> AddAsync<DObject>([FromBody] DObject Dto)
        {
            //TODO : Convert DTO DObject to T object
            //T tmpObj = new T();

            Result<int> result = await Dispatcher.SendCommandAsync<AddStockCommand<T>, int>(
                new AddStockCommand<T>(null)
            );

            return this.FromResult(result);
        }

        [Route("DeleteAsync")]
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            Result result = await Dispatcher.SendCommandAsync<DeleteStockCommand<T>>(
                new DeleteStockCommand<T>(id)
            );

            return this.FromResult(result);
        }
    }
}
