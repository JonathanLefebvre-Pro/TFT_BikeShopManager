using BikeShopManager.AppCore.Interfaces.Queries;

namespace BikeShopManager.AppCore.Features.Queries.Customers;

public record GetCustomersCountQuery() : IQueryDefinition<int>;
