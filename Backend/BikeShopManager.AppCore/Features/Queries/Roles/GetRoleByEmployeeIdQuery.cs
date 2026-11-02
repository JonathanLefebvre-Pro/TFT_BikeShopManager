using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Roles;

public record GetRoleByEmployeeIdQuery(int Id) : IQueryDefinition<Role>;
