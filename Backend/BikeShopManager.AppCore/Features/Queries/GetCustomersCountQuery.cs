using BikeShopManager.AppCore.Interfaces.Queries;

namespace BikeShopManager.AppCore.Features.Queries;

public record GetCustomersCountQuery() : IQueryDefinition<int>;
