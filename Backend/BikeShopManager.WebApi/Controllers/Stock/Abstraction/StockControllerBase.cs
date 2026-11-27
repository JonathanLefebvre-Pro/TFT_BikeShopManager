using BikeShopManager.AppCore.Features.Commands.Stock;
using BikeShopManager.AppCore.Features.Queries.Stock;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Extensions.Controller;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock.Abstraction
{
    [ApiController]
    public abstract class StockControllerBase<TEntity> : ControllerBase
        where TEntity : StockEntityBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly IDtoMapper<TEntity, IResponseDto<TEntity>> _responseDtoMapper;

        public StockControllerBase(
            IDispatcher Dispatcher,
            IDtoMapper<TEntity, IResponseDto<TEntity>> ResponseDtoMapper
        )
        {
            _dispatcher = Dispatcher;
            _responseDtoMapper = ResponseDtoMapper;
        }

        [Route("GetAsync")]
        [HttpGet]
        //[Authorize(Roles = ("Admin, Manager, Seller, Mechanic"))]
        public virtual async Task<IActionResult> GetAsync()
        {
            Result<IEnumerable<TEntity>> result = await _dispatcher.SendQueryAsync<
                GetStockQuery<TEntity>,
                IEnumerable<TEntity>
            >(new GetStockQuery<TEntity>());

            if (result.IsSuccess)
            {
                IEnumerable<IResponseDto<TEntity>> dtos = _responseDtoMapper.ToDtos(result.Data);
                return this.Ok(dtos);
            }

            return this.NotFound(result.Error);
        }

        [Route("GetByIdAsync/{id}")]
        [HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(int id)
        {
            Result<TEntity> result = await _dispatcher.SendQueryAsync<
                GetStockByIdQuery<TEntity>,
                TEntity
            >(new GetStockByIdQuery<TEntity>(id));

            if (result.IsSuccess)
            {
                IResponseDto<TEntity> dto = _responseDtoMapper.ToDto(result.Data);
                return this.Ok(dto);
            }

            return this.NotFound(result.Error);
        }

        [Route("GetByNameAsync/{name}")]
        [HttpGet]
        public virtual async Task<IActionResult> GetByNameAsync(string name)
        {
            Result<TEntity> result = await _dispatcher.SendQueryAsync<
                GetStockByNameQuery<TEntity>,
                TEntity
            >(new GetStockByNameQuery<TEntity>(name));

            if (result.IsSuccess)
            {
                IResponseDto<TEntity> dto = _responseDtoMapper.ToDto(result.Data);
                return this.Ok(dto);
            }

            return this.FromResult(result);
        }

        [Route("DeleteAsync/{id}")]
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            Result result = await _dispatcher.SendCommandAsync<DeleteStockCommand<TEntity>>(
                new DeleteStockCommand<TEntity>(id)
            );

            return this.FromResult(result);
        }

        protected async Task<IActionResult> AddAsyncCore(TEntity entity)
        {
            Result<int> result = await _dispatcher.SendCommandAsync<AddStockCommand<TEntity>, int>(
                new AddStockCommand<TEntity>(entity)
            );

            return this.FromResult(result);
        }

        protected async Task<IActionResult> UpdateAsyncCore(int id, TEntity entity)
        {
            Result result = await _dispatcher.SendCommandAsync<UpdateStockCommand<TEntity>>(
                new UpdateStockCommand<TEntity>(id, entity)
            );

            return this.FromResult(result);
        }
    }
}
