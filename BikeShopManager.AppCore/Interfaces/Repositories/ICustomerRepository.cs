using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface ICustomerRepository
    : IQueryHandlerAsync<GetCustomerByIdQuery, Customer>,
        ICommandHandlerAsync<AddCustomerCommand, int>,
        ICommandHandlerAsync<DisableCustomerCommand>,
        ICommandHandlerAsync<UpdateCustomerCommand>;
