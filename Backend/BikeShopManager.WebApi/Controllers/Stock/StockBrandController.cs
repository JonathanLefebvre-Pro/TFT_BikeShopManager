using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
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
        public StockBrandController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
