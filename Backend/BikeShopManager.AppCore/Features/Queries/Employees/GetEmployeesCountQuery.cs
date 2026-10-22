using BikeShopManager.AppCore.Interfaces.Queries;

namespace BikeShopManager.AppCore.Features.Queries;

public record GetEmployeesCountQuery() : IQueryDefinition<int>;
