using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Controllers.Stock.Abstraction;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/VatRate")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("VatRate")]
    public class StockVatRateController : StockControllerBase<VatRate>
    {
        public StockVatRateController(
            IDispatcher Dispatcher,
            IDtoMapper<VatRate, IResponseDto<VatRate>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }
    }
}
