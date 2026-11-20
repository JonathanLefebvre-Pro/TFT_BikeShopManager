using BikeShopManager.AppCore.Features.Commands.Employees;
using BikeShopManager.AppCore.Features.Queries.Employees;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Tokens;
using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;
using BikeShopManager.WebApi.Extensions.Controller;
using BikeShopManager.WebApi.Mappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ("Admin, Manager"))]
    public class EmployeeController(IDispatcher Dispatcher, IJwtUtilities JwtUtilities)
        : ControllerBase
    {
        [Route("GetAsync")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery] string? keywords,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            Result<PageResponse<Employee>> result = await Dispatcher.SendQueryAsync<
                GetEmployeesQuery,
                PageResponse<Employee>
            >(new GetEmployeesQuery(pageNumber, pageSize, keywords));

            if (result.IsSuccess)
            {
                PageResponseDto<EmployeeResponseDto> responseDto =
                    new PageResponseDto<EmployeeResponseDto>()
                    {
                        Data = result.Data.Data.ToIEnumerableEmployeeResponseDtos(),
                        PageSize = result.Data.PageSize,
                        PageNumber = result.Data.PageNumber,
                        TotalRecords = result.Data.TotalRecords,
                        TotalPages = result.Data.TotalPages,
                    };

                return this.Ok(responseDto);
            }

            return this.NotFound(result.Error);
        }

        [Route("GetByRoleIdAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByRoleIdAsync(int id)
        {
            Result<IEnumerable<Employee>> result = await Dispatcher.SendQueryAsync<
                GetEmployeesByRoleIdQuery,
                IEnumerable<Employee>
            >(new GetEmployeesByRoleIdQuery(id));

            if (result.IsSuccess)
            {
                List<EmployeeResponseDto> dtos = new List<EmployeeResponseDto>();
                foreach (Employee employee in result.Data)
                    dtos.Add(employee.ToEmployeeResponseDto());

                return this.Ok(dtos);
            }

            return this.NotFound(result.Error);
        }

        [Route("GetCountAsync")]
        [HttpGet]
        public async Task<IActionResult> GetCountAsync()
        {
            Result<int> result = await Dispatcher.SendQueryAsync<GetEmployeesCountQuery, int>(
                new GetEmployeesCountQuery()
            );

            return this.FromResult(result);
        }

        [Route("GetAsync/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Result<Employee> result = await Dispatcher.SendQueryAsync<
                GetEmployeeByIdQuery,
                Employee
            >(new GetEmployeeByIdQuery(id));

            if (result.IsSuccess)
                return this.Ok(result.Data.ToEmployeeResponseDto());

            return this.NotFound(result.Error);
        }

        [Route("UpdateAsync/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(
            int id,
            [FromBody] EmployeeUpdateDto dto,
            [FromServices] IValidator<EmployeeUpdateDto> validator,
            CancellationToken cancellationToken
        )
        {
            FluentValidation.Results.ValidationResult validationResult =
                await validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                return this.ValidationProblem(
                    new ValidationProblemDetails(validationResult.ToDictionary())
                );

            Result result = await Dispatcher.SendCommandAsync<UpdateEmployeeCommand>(
                new UpdateEmployeeCommand(
                    id,
                    dto.ToEmployeeEntity(),
                    dto.ToUserEntity(),
                    dto.RoleId
                )
            );
            return this.FromResult(result);
        }

        [Route("UpdatePasswordAsync/{id}")]
        [HttpPatch]
        public async Task<IActionResult> UpdatePasswordAsync(
            int id,
            [FromBody] AuthUserUpdatePasswordDto dto,
            [FromServices] IValidator<AuthUserUpdatePasswordDto> validator,
            CancellationToken cancellationToken
        )
        {
            FluentValidation.Results.ValidationResult validationResult =
                await validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                return this.ValidationProblem(
                    new ValidationProblemDetails(validationResult.ToDictionary())
                );

            Result result = await Dispatcher.SendCommandAsync<UpdateEmployeePasswordCommand>(
                new UpdateEmployeePasswordCommand(id, dto.OldPassword, dto.NewPassword)
            );
            return this.FromResult(result);
        }

        [Route("DisableAsync/{id}")]
        [HttpPatch]
        public async Task<IActionResult> DisableAsync(int id)
        {
            //Get the employee id from the JWT claims
            int authUserId = JwtUtilities.GetClaimSub(User);
            //An Auth User cannot disable itself !
            if (id == authUserId)
                return this.Forbid();

            Result result = await Dispatcher.SendCommandAsync<DisableEmployeeCommand>(
                new DisableEmployeeCommand(id)
            );
            return this.FromResult(result);
        }

        [Route("EnableAsync/{id}")]
        [HttpPatch]
        public async Task<IActionResult> EnableAsync(int id)
        {
            //Get the employee id from the JWT claims
            int authUserId = JwtUtilities.GetClaimSub(User);
            //An Auth User cannot enable itself !
            if (id == authUserId)
                return this.Forbid();

            Result result = await Dispatcher.SendCommandAsync<EnableEmployeeCommand>(
                new EnableEmployeeCommand(id)
            );
            return this.FromResult(result);
        }

        [Route("AddAsync")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(
            [FromBody] EmployeeAddDto dto,
            [FromServices] IValidator<EmployeeAddDto> validator,
            CancellationToken cancellationToken
        )
        {
            FluentValidation.Results.ValidationResult validationResult =
                await validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                return this.ValidationProblem(
                    new ValidationProblemDetails(validationResult.ToDictionary())
                );

            Result<int> result = await Dispatcher.SendCommandAsync<AddEmployeeCommand, int>(
                new AddEmployeeCommand(dto.ToEmployeeEntity(), dto.ToUserEntity(), dto.RoleId)
            );

            return this.FromResult(result);
        }
    }
}
