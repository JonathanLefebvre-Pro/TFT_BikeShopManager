using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.Infrastructure.Services.Tokens;

public class JwtBearerEventsValidator : JwtBearerEvents
{
    public override async Task TokenValidated(TokenValidatedContext context)
    {
        await base.TokenValidated(context);

        // Perform additional security
        // check security stamp and isValid claims
        ClaimsPrincipal? principal = context.Principal;

        //Get employee id and security stamp from the token claims
        string? employeeId = principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);
        string? securityStamp = principal?.FindFirstValue("SecurityStamp");

        if (String.IsNullOrEmpty(employeeId))
        {
            context.Fail("Invalid employee id !");
            return;
        }

        if (String.IsNullOrEmpty(securityStamp))
        {
            context.Fail("Invalid security stamp !");
            return;
        }

        IEmployeeRepository employeeRepository =
            context.HttpContext.RequestServices.GetRequiredService<IEmployeeRepository>();
        //Get the employee from the employee id
        Employee? employee = await employeeRepository.GetEmployeeById(int.Parse(employeeId));

        //Fail if the employee is null, not active or the security stamp does not match
        if (employee is null || !employee.IsActive || employee.SecurityStamp != securityStamp)
        {
            context.Fail("Invalid token !");
        }
    }
}
