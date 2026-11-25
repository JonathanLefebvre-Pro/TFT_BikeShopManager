using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockBikeModelController : StockActivableControllerBase<BikeModel>
    {
        public StockBikeModelController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
