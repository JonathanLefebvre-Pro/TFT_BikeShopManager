using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
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
        public StockBikeModelController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
