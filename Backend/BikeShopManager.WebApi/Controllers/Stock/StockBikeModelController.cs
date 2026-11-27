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
    [Route("api/Stock/BikeModel")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("BikeModel")]
    public class StockBikeModelController
        : StockActivableControllerBase<BikeModel>,
            IControllerAddAsync<StockBikeModelAddDto>,
            IControllerUpdateAsync<StockBikeModelUpdateDto>
    {
        public StockBikeModelController(
            IDispatcher Dispatcher,
            IDtoMapper<BikeModel, IResponseDto<BikeModel>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }

        [Route("AddAsync")]
        [HttpPost]
        public Task<IActionResult> AddAsync([FromBody] StockBikeModelAddDto dto)
        {
            throw new NotImplementedException();
        }

        [Route("UpdateAsync")]
        [HttpPut]
        public Task<IActionResult> UpdateAsync(int id, [FromBody] StockBikeModelUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
