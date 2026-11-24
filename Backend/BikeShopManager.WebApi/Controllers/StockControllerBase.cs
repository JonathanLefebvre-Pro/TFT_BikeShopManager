using BikeShopManager.AppCore.Features.Queries.Stock;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Extensions.Controller;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
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
    }
}
