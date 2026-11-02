using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Employees;

public record GetEmployeesByRoleIdQuery(int Id) : IQueryDefinition<IEnumerable<Employee>>;
