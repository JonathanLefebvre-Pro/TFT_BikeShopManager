using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Controllers.Stock.Abstraction;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/BikeModel")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("BikeModel")]
    public class StockBikeModelController : StockActivableControllerBase<BikeModel>
    {
        public StockBikeModelController(
            IDispatcher Dispatcher,
            IDtoMapper<BikeModel, IResponseDto<BikeModel>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }
    }
}
