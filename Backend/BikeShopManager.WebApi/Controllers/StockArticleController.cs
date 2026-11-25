using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockArticleController : StockActivableControllerBase<Article>
    {
        public StockArticleController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
