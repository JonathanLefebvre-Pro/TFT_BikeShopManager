using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Roles;

public class GetRoleByIdQueryHandler : IQueryHandlerAsync<GetRoleByIdQuery, Role>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Result<Role>> HandleAsync(GetRoleByIdQuery query)
    {
        Role? role = await _roleRepository.GetRoleById(query.Id);

        if (role is null)
            return RoleError.NotFound;

        return role;
    }
}
