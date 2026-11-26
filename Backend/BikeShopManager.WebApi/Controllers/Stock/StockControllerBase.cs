using BikeShopManager.AppCore.Features.Commands.Stock;
using BikeShopManager.AppCore.Features.Queries.Stock;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Extensions.Controller;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [ApiController]
    public abstract class StockControllerBase<TEntity>(IDispatcher Dispatcher) : ControllerBase
        where TEntity : StockEntityBase
    {
        [Route("GetAsync")]
        [HttpGet]
        //[Authorize(Roles = ("Admin, Manager, Seller, Mechanic"))]
        public virtual async Task<IActionResult> GetAsync()
        {
            Result<IEnumerable<TEntity>> result = await Dispatcher.SendQueryAsync<
                GetStockQuery<TEntity>,
                IEnumerable<TEntity>
            >(new GetStockQuery<TEntity>());

            return this.FromResult(result);
        }

        [Route("GetByIdAsync/{id}")]
        [HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(int id)
        {
            Result<TEntity> result = await Dispatcher.SendQueryAsync<
                GetStockByIdQuery<TEntity>,
                TEntity
            >(new GetStockByIdQuery<TEntity>(id));

            return this.FromResult(result);
        }

        [Route("GetByNameAsync/{name}")]
        [HttpGet]
        public virtual async Task<IActionResult> GetByNameAsync(string name)
        {
            Result<TEntity> result = await Dispatcher.SendQueryAsync<
                GetStockByNameQuery<TEntity>,
                TEntity
            >(new GetStockByNameQuery<TEntity>(name));

            return this.FromResult(result);
        }

        [Route("DeleteAsync/{id}")]
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            Result result = await Dispatcher.SendCommandAsync<DeleteStockCommand<TEntity>>(
                new DeleteStockCommand<TEntity>(id)
            );

            return this.FromResult(result);
        }

        protected async Task<IActionResult> AddAsyncCore<TAddDto>(TAddDto Dto)
        {
            //TODO : Convert DTO TObject to T object
            //T tmpObj = new T();

            Result<int> result = await Dispatcher.SendCommandAsync<AddStockCommand<TEntity>, int>(
                new AddStockCommand<TEntity>(null)
            );

            return this.FromResult(result);
        }

        protected async Task<IActionResult> UpdateAsyncCore<TUpdateDto>(int id, TUpdateDto Dto)
        {
            //TODO : Convert DTO TObject to T object
            //T tmpObj = new T();

            Result result = await Dispatcher.SendCommandAsync<UpdateStockCommand<TEntity>>(
                new UpdateStockCommand<TEntity>(id, null)
            );

            return this.FromResult(result);
        }
    }
}
