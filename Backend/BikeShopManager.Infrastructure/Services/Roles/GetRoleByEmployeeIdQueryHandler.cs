using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services.Roles;

public class GetRoleByEmployeeIdQueryHandler(
    IEmployeeRepository EmployeeRepository,
    IEmployeeRoleRepository EmployeeRoleRepository,
    IRoleRepository RoleRepository
) : IQueryHandlerAsync<GetRoleByEmployeeIdQuery, Role>
{
    public async Task<Result<Role>> HandleAsync(GetRoleByEmployeeIdQuery query)
    {
        //Check if employee exist
        bool isEmployeeExist = await EmployeeRepository.IsEmployeeExist(query.Id);
        if (!isEmployeeExist)
            return Result<Role>.Failure(EmployeeError.NotFound);

        //Get roleId from the Persons.EmployeeRole table
        int roleId = await EmployeeRoleRepository.GetEmployeeRole(query.Id);
        if (roleId is 0)
            Result<Role>.Failure(RoleError.NotFound);

        //Get the role from the Persons.Role table
        Role? role = await RoleRepository.GetRoleById(roleId);

        return role is not null
            ? Result<Role>.Success(role)
            : Result<Role>.Failure(RoleError.NotFound);
    }
}
