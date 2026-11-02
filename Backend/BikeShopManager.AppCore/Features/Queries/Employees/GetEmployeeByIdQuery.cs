using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Employees;

public record GetEmployeeByIdQuery(int Id) : IQueryDefinition<Employee>;
