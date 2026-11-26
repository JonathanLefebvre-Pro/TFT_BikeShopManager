using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/VatRate")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("VatRate")]
    public class StockVatRateController : StockControllerBase<VatRate>
    {
        public StockVatRateController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
