using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Controllers.Stock.Abstraction;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/Category")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Category")]
    public class StockCategoryController : StockControllerBase<Category>
    {
        public StockCategoryController(
            IDispatcher Dispatcher,
            IDtoMapper<Category, IResponseDto<Category>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }
    }
}
