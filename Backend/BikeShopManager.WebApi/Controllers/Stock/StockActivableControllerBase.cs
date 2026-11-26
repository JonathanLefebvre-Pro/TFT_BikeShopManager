using BikeShopManager.AppCore.Features.Commands.Stock;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Shared;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Extensions.Controller;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [ApiController]
    public abstract class StockActivableControllerBase<T> : StockControllerBase<T>
        where T : StockEntityBase, IActivable
    {
        private IDispatcher Dispatcher;

        public StockActivableControllerBase(IDispatcher Dispatcher)
            : base(Dispatcher)
        {
            this.Dispatcher = Dispatcher;
        }

        [Route("EnableAsync/{id}")]
        [HttpPatch]
        //[Authorize(Roles = ("Admin, Manager, Seller, Mechanic"))]
        public virtual async Task<IActionResult> EnableAsync(int id)
        {
            Result result = await Dispatcher.SendCommandAsync<EnableStockCommand<T>>(
                new EnableStockCommand<T>(id)
            );

            return this.FromResult(result);
        }

        [Route("DisableAsync/{id}")]
        [HttpPatch]
        //[Authorize(Roles = ("Admin, Manager, Seller, Mechanic"))]
        public virtual async Task<IActionResult> DisableAsync(int id)
        {
            Result result = await Dispatcher.SendCommandAsync<DisableStockCommand<T>>(
                new DisableStockCommand<T>(id)
            );

            return this.FromResult(result);
        }
    }
}
