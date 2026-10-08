using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IEmployeeRepository
    : IQueryHandlerAsync<GetEmployeeByIdQuery, Employee>,
        ICommandHandlerAsync<AddEmployeeCommand, int>,
        ICommandHandlerAsync<DisableEmployeeCommand>,
        ICommandHandlerAsync<EnableEmployeeCommand>,
        ICommandHandlerAsync<UpdateEmployeeCommand>;
