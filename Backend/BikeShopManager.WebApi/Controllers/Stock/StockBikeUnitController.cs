using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/BikeUnit")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("BikeUnit")]
    public class StockBikeUnitController : StockControllerBase<BikeUnit>
    {
        public StockBikeUnitController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
