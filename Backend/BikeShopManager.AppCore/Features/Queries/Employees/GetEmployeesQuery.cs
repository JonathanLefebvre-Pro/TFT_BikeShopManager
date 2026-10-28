using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Employees;

public record GetEmployeesQuery(int PageNumber, int PageSize, string? Keywords)
    : IQueryDefinition<PageResponse<Employee>>;
