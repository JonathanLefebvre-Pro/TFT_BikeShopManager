using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;
using BikeShopManager.WebApi.Infrastructure.Extensions;
using BikeShopManager.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(IDispatcher Dispatcher) : ControllerBase
    {
        [Route("GetAsync")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            Result<PageResponse<Customer>> result = await Dispatcher.SendQueryAsync<
                GetCustomersQuery,
                PageResponse<Customer>
            >(new GetCustomersQuery(pageNumber, pageSize));

            if (result.IsSuccess)
            {
                PageResponseDto<CustomerReturnDto> responseDto =
                    new PageResponseDto<CustomerReturnDto>()
                    {
                        Data = result.Data.Data.ToIEnumerableCustomerReturnDtos(),
                        PageSize = result.Data.PageSize,
                        PageNumber = result.Data.PageNumber,
                        TotalRecords = result.Data.TotalRecords,
                        TotalPages = result.Data.TotalPages,
                    };

                return this.Ok(responseDto);
            }

            return this.NotFound(result.Error);
        }

        [Route("GetCountAsync")]
        [HttpGet]
        public async Task<IActionResult> GetCountAsync()
        {
            Result<int> result = await Dispatcher.SendQueryAsync<GetCustomersCountQuery, int>(
                new GetCustomersCountQuery()
            );

            return this.FromResult(result);
        }

        [Route("GetAsyncByKeywords")]
        [HttpPost]
        public async Task<IActionResult> GetAsyncByKeywords(
            [FromBody] CustomerKeywordsDto dto,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            Result<PageResponse<Customer>> result = await Dispatcher.SendQueryAsync<
                GetCustomersByKeywordsQuery,
                PageResponse<Customer>
            >(new GetCustomersByKeywordsQuery(pageNumber, pageSize, dto.Keywords));

            if (result.IsSuccess)
            {
                PageResponseDto<CustomerReturnDto> responseDto =
                    new PageResponseDto<CustomerReturnDto>()
                    {
                        Data = result.Data.Data.ToIEnumerableCustomerReturnDtos(),
                        PageSize = result.Data.PageSize,
                        PageNumber = result.Data.PageNumber,
                        TotalRecords = result.Data.TotalRecords,
                        TotalPages = result.Data.TotalPages,
                    };

                return this.Ok(responseDto);
            }

            return this.NotFound(result.Error);
        }

        [Route("GetAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Result<Customer> result = await Dispatcher.SendQueryAsync<
                GetCustomerByIdQuery,
                Customer
            >(new GetCustomerByIdQuery(id));

            //Convert data to dto
            if (result.IsSuccess)
                return this.Ok(result.Data.ToCustomerReturnDto());

            return this.NotFound(result.Error);
        }

        [Route("UpdateAsync/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CustomerUpdateDto dto)
        {
            Result result = await Dispatcher.SendCommandAsync<UpdateCustomerCommand>(
                new UpdateCustomerCommand(id, dto.ToCustomerEntity(), dto.ToUserEntity())
            );
            return this.FromResult(result);
        }

        [Route("DisableAsync/{id}")]
        [HttpPatch]
        public async Task<IActionResult> DisableAsync(int id)
        {
            Result result = await Dispatcher.SendCommandAsync<DisableCustomerCommand>(
                new DisableCustomerCommand(id)
            );
            return this.FromResult(result);
        }

        [Route("EnableAsync/{id}")]
        [HttpPatch]
        public async Task<IActionResult> EnableAsync(int id)
        {
            Result result = await Dispatcher.SendCommandAsync<EnableCustomerCommand>(
                new EnableCustomerCommand(id)
            );
            return this.FromResult(result);
        }

        [Route("AddAsync")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CustomerAddDto dto)
        {
            Result<int> result = await Dispatcher.SendCommandAsync<AddCustomerCommand, int>(
                new AddCustomerCommand(dto.ToCustomerEntity(), dto.ToUserEntity())
            );

            return this.FromResult(result);
        }
    }
}
