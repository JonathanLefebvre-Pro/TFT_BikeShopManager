using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries;

public record GetCustomersByKeywordsQuery(int PageNumber, int PageSize, string[] keywords)
    : IQueryDefinition<PageResponse<Customer>>;
