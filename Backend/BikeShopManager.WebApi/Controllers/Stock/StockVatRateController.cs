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
    [Route("api/Stock/VatRate")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("VatRate")]
    public class StockVatRateController
        : StockControllerBase<VatRate>,
            IControllerAddAsync<StockVatRateAddDto>,
            IControllerUpdateAsync<StockVatRateUpdateDto>
    {
        public StockVatRateController(
            IDispatcher Dispatcher,
            IDtoMapper<VatRate, IResponseDto<VatRate>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }

        [Route("AddAsync")]
        [HttpPost]
        public Task<IActionResult> AddAsync([FromBody] StockVatRateAddDto dto)
        {
            throw new NotImplementedException();
        }

        [Route("UpdateAsync")]
        [HttpPut]
        public Task<IActionResult> UpdateAsync(int id, [FromBody] StockVatRateUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
