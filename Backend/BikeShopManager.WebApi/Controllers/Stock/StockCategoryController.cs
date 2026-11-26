using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
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
        public StockCategoryController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
