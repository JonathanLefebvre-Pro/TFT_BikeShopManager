using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Controllers.Stock.Abstraction;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/Location")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Location")]
    public class StockLocationController : StockControllerBase<Location>
    {
        public StockLocationController(
            IDispatcher Dispatcher,
            IDtoMapper<Location, IResponseDto<Location>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }
    }
}
