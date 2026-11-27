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
    [Route("api/Stock/Image")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Image")]
    public class StockImageController
        : StockControllerBase<Image>,
            IControllerAddAsync<StockImageAddDto>,
            IControllerUpdateAsync<StockImageUpdateDto>
    {
        public StockImageController(
            IDispatcher Dispatcher,
            IDtoMapper<Image, IResponseDto<Image>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }

        [Route("AddAsync")]
        [HttpPost]
        public Task<IActionResult> AddAsync([FromBody] StockImageAddDto dto)
        {
            throw new NotImplementedException();
        }

        [Route("UpdateAsync")]
        [HttpPut]
        public Task<IActionResult> UpdateAsync(int id, [FromBody] StockImageUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
