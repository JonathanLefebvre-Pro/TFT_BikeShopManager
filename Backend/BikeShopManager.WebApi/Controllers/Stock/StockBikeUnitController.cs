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
    [Route("api/Stock/BikeUnit")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("BikeUnit")]
    public class StockBikeUnitController
        : StockControllerBase<BikeUnit>,
            IControllerAddAsync<StockBikeUnitAddDto>,
            IControllerUpdateAsync<StockBikeUnitUpdateDto>
    {
        public StockBikeUnitController(
            IDispatcher Dispatcher,
            IDtoMapper<BikeUnit, IResponseDto<BikeUnit>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }

        [Route("AddAsync")]
        [HttpPost]
        public Task<IActionResult> AddAsync([FromBody] StockBikeUnitAddDto dto)
        {
            throw new NotImplementedException();
        }

        [Route("UpdateAsync")]
        [HttpPut]
        public Task<IActionResult> UpdateAsync(int id, [FromBody] StockBikeUnitUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
