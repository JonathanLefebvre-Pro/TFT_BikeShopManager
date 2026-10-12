using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries;

public record GetEmployeesByRoleIdQuery(int Id) : IQueryDefinition<IEnumerable<Employee>>;
