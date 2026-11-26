using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/Image")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Image")]
    public class StockImageController : StockControllerBase<Image>
    {
        public StockImageController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
