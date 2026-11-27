using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Controllers.Stock.Abstraction;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/Brand")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Brand")]
    public class StockBrandController : StockControllerBase<Brand>
    {
        public StockBrandController(
            IDispatcher Dispatcher,
            IDtoMapper<Brand, IResponseDto<Brand>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }
    }
}
