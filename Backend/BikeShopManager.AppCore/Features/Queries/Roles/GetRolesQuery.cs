using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Roles;

public record GetRolesQuery() : IQueryDefinition<IEnumerable<Role>>;
