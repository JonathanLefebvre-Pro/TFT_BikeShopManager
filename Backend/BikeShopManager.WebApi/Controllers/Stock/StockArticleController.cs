using BikeShopManager.AppCore.Interfaces.Controllers;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Dtos;
using BikeShopManager.AppCore.Interfaces.Mappers;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Controllers.Stock.Abstraction;
using BikeShopManager.WebApi.Dtos.Stock;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers.Stock
{
    [Route("api/Stock/Article")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Article")]
    public class StockArticleController
        : StockActivableControllerBase<Article>,
            IControllerAddAsync<StockArticleAddDto>,
            IControllerUpdateAsync<StockArticleUpdateDto>
    {
        private readonly IEntityMapper<Article, StockArticleAddDto> _addEntityMapper;
        private readonly IEntityMapper<Article, StockArticleUpdateDto> _updateEntityMapper;

        public StockArticleController(
            IDispatcher Dispatcher,
            IEntityMapper<Article, StockArticleAddDto> AddEntityMapper,
            IEntityMapper<Article, StockArticleUpdateDto> UpdateEntityMapper,
            IDtoMapper<Article, IResponseDto<Article>> ResponseDtomMapper
        )
            : base(Dispatcher, ResponseDtomMapper)
        {
            _addEntityMapper = AddEntityMapper;
            _updateEntityMapper = UpdateEntityMapper;
        }

        [Route("AddAsync")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] StockArticleAddDto dto)
        {
            Article article = _addEntityMapper.ToEntity(dto);

            return await AddAsyncCore(article);
        }

        [Route("UpdateAsync")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] StockArticleUpdateDto dto)
        {
            Article article = _updateEntityMapper.ToEntity(dto);

            return await UpdateAsyncCore(id, article);
        }
    }
}
