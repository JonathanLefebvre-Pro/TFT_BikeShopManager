using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Auth;

public record LoginQuery(string Login, string Password) : IQueryDefinition<AuthUser>;
