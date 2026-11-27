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
    [Route("api/Stock/Category")]
    [ApiController]
    [TagGroup("Stock")]
    [Tags("Category")]
    public class StockCategoryController
        : StockControllerBase<Category>,
            IControllerAddAsync<StockCategoryAddDto>,
            IControllerUpdateAsync<StockCategoryUpdateDto>
    {
        public StockCategoryController(
            IDispatcher Dispatcher,
            IDtoMapper<Category, IResponseDto<Category>> ResponseDtoMapper
        )
            : base(Dispatcher, ResponseDtoMapper) { }

        [Route("AddAsync")]
        [HttpPost]
        public Task<IActionResult> AddAsync([FromBody] StockCategoryAddDto dto)
        {
            throw new NotImplementedException();
        }

        [Route("UpdateAsync")]
        [HttpPut]
        public Task<IActionResult> UpdateAsync(int id, [FromBody] StockCategoryUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
