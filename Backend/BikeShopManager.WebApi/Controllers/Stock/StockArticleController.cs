using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/Article")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Article")]
    public class StockArticleController : StockActivableControllerBase<Article>
    {
        public StockArticleController(IDispatcher Dispatcher)
            : base(Dispatcher) { }
    }
}
