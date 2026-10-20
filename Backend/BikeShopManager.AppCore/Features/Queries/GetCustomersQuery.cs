using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries;

public record GetCustomersQuery(int Offset, int Next) : IQueryDefinition<IEnumerable<Customer>>;
