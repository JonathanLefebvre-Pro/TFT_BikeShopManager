using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Customers;

public record GetCustomerByIdQuery(int Id) : IQueryDefinition<Customer>;
