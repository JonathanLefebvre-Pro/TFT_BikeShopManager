using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockLocationController : StockControllerBase<Location>
    {
        public StockLocationController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
