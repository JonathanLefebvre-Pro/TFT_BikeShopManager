using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Dtos.Stock;
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

        [Route("AddAsync")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] StockAddArticleDto Dto)
        {
            return await AddAsyncCore<StockAddArticleDto>(Dto);
        }

        [Route("UpdateAsync")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] StockUpdateArticleDto Dto)
        {
            return await UpdateAsyncCore<StockUpdateArticleDto>(id, Dto);
        }
    }
}
