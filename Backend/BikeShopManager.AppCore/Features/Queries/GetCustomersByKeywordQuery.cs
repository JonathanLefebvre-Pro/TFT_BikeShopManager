using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries;

public record GetCustomersByKeywordQuery(string keyword) : IQueryDefinition<IEnumerable<Customer>>;
