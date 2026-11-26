using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/Location")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Lcoation")]
    public class StockLocationController : StockControllerBase<Location>
    {
        public StockLocationController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
