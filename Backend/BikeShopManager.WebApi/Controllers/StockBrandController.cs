using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockBrandController : StockControllerBase<Brand>
    {
        public StockBrandController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
