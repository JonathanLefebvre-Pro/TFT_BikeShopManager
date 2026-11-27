using BikeShopManager.AppCore.Interfaces.Controllers;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Controllers.Stock.Abstraction;
using BikeShopManager.WebApi.Dtos.Stock;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/Brand")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Brand")]
    public class StockBrandController
        : StockControllerBase<Brand>,
            IControllerAddAsync<StockBrandAddDto>,
            IControllerUpdateAsync<StockBrandUpdateDto>
    {
        private readonly IEntityMapper<Brand, StockBrandAddDto> _addEntityMapper;
        private readonly IEntityMapper<Brand, StockBrandUpdateDto> _updateEntityMapper;

        public StockBrandController(
            IDispatcher Dispatcher,
            IEntityMapper<Brand, StockBrandAddDto> AddEntityMapper,
            IEntityMapper<Brand, StockBrandUpdateDto> UpdateEntityMapper,
            IDtoMapper<Brand, IResponseDto<Brand>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper)
        {
            _addEntityMapper = AddEntityMapper;
            _updateEntityMapper = UpdateEntityMapper;
        }

        [Route("AddAsync")]
        [HttpPost]
        public Task<IActionResult> AddAsync([FromBody] StockBrandAddDto dto)
        {
            throw new NotImplementedException();
        }

        [Route("UpdateAsync")]
        [HttpPut]
        public Task<IActionResult> UpdateAsync(int id, [FromBody] StockBrandUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
