using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries;

public record GetCustomersQuery(int Offset, int Limit) : IQueryDefinition<IEnumerable<Customer>>;
