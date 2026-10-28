using BikeShopManager.AppCore.Interfaces.Queries;

namespace BikeShopManager.AppCore.Features.Queries.Employees;

public record GetEmployeesCountQuery() : IQueryDefinition<int>;
