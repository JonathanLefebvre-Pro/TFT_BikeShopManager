using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Roles;

public class GetRolesQueryHandler : IQueryHandlerAsync<GetRolesQuery, IEnumerable<Role>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Result<IEnumerable<Role>>> HandleAsync(GetRolesQuery query)
    {
        IEnumerable<Role> roles = await _roleRepository.GetRoles();

        if (roles is null || roles.Count() is 0)
            return RoleError.Empty;

        return Result<IEnumerable<Role>>.Success(roles);
    }
}
