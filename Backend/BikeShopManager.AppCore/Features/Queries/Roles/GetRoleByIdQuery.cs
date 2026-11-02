using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Roles;

public record GetRoleByIdQuery(int Id) : IQueryDefinition<Role>;
