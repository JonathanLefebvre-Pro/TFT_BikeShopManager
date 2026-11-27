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
    [Route("api/Stock/Location")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Location")]
    public class StockLocationController
        : StockControllerBase<Location>,
            IControllerAddAsync<StockLocationAddDto>,
            IControllerUpdateAsync<StockLocationUpdateDto>
    {
        public StockLocationController(
            IDispatcher Dispatcher,
            IDtoMapper<Location, IResponseDto<Location>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }

        [Route("AddAsync")]
        [HttpPost]
        public Task<IActionResult> AddAsync([FromBody] StockLocationAddDto dto)
        {
            throw new NotImplementedException();
        }

        [Route("UpdateAsync")]
        [HttpPut]
        public Task<IActionResult> UpdateAsync(int id, [FromBody] StockLocationUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
