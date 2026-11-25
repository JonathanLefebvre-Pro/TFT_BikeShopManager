using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockVatRateController : StockControllerBase<VatRate>
    {
        public StockVatRateController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
