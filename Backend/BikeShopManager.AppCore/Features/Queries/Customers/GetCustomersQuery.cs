using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Customers;

public record GetCustomersQuery(int PageNumber, int PageSize, string? Keywords)
    : IQueryDefinition<PageResponse<Customer>>;
