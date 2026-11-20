using BikeShopManager.AppCore.Features.Commands.Employees;
using BikeShopManager.AppCore.Features.Queries.Employees;
using BikeShopManager.AppCore.Features.Queries.Roles;
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
    [Authorize]
    public class AuthUserController(IDispatcher Dispatcher, IJwtUtilities JwtUtilities, IValidator<AuthUserUpdatePasswordDto> Validator)
        : ControllerBase
    {
        [Route("GetUserAsync")]
        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            //Get the employee id from the JWT claims
            int id = JwtUtilities.GetClaimSub(User);

            //Get the employee data
            Result<Employee> result = await Dispatcher.SendQueryAsync<
                GetEmployeeByIdQuery,
                Employee
            >(new GetEmployeeByIdQuery(id));

            if (result.IsSuccess)
                return this.Ok(result.Data.ToEmployeeResponseDto());

            return this.NotFound(result.Error);
        }

        [Route("UpdatePasswordAsync")]
        [HttpPatch]
        public async Task<IActionResult> UpdatePasswordAsync(
            [FromBody] AuthUserUpdatePasswordDto dto, CancellationToken cancellationToken
        )
        {
            FluentValidation.Results.ValidationResult validationResult = await Validator.ValidateAsync(dto, cancellationToken);
            if(!validationResult.IsValid) return this.ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));

            //Get the employee id from the JWT claims
            int id = JwtUtilities.GetClaimSub(User);

            Result result = await Dispatcher.SendCommandAsync<UpdateEmployeePasswordCommand>(
                new UpdateEmployeePasswordCommand(id, dto.OldPassword, dto.NewPassword)
            );

            return this.FromResult(result);
        }

        [Route("GetRoleAsync")]
        [HttpGet]
        public async Task<IActionResult> GetRoleAsync()
        {
            //Get the employee id from the JWT claims
            int id = JwtUtilities.GetClaimSub(User);

            Result<Role> result = await Dispatcher.SendQueryAsync<GetRoleByEmployeeIdQuery, Role>(
                new GetRoleByEmployeeIdQuery(id)
            );

            if (result.IsSuccess)
                return this.Ok(result.Data.ToRoleResponseDto());

            return this.NotFound(result.Error);
        }
    }
}
