using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.AppCore.Features.Dispatcher;

public class Dispatcher(IServiceProvider ServiceProvider) : IDispatcher
{
    public Result SendCommand<TCommand>(TCommand command)
        where TCommand : ICommandDefinition
    {
        return ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>().Handle(command);
    }

    public Result<TResult> SendCommand<TCommand, TResult>(TCommand command)
        where TCommand : ICommandDefinition<TResult>
    {
        return ServiceProvider
            .GetRequiredService<ICommandHandler<TCommand, TResult>>()
            .Handle(command);
    }

    public async Task<Result> SendCommandAsync<TCommand>(TCommand command)
        where TCommand : ICommandDefinition
    {
        return await ServiceProvider
            .GetRequiredService<ICommandHandlerAsync<TCommand>>()
            .HandleAsync(command);
    }

    public async Task<Result<TResult>> SendCommandAsync<TCommand, TResult>(TCommand command)
        where TCommand : ICommandDefinition<TResult>
    {
        return await ServiceProvider
            .GetRequiredService<ICommandHandlerAsync<TCommand, TResult>>()
            .HandleAsync(command);
    }

    public Result<TResult> SendQuery<TQuery, TResult>(TQuery query)
        where TQuery : IQueryDefinition<TResult>
    {
        return ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>().Handle(query);
    }

    public async Task<Result<TResult>> SendQueryAsync<TQuery, TResult>(TQuery query)
        where TQuery : IQueryDefinition<TResult>
    {
        return await ServiceProvider
            .GetRequiredService<IQueryHandlerAsync<TQuery, TResult>>()
            .HandleAsync(query);
    }
}
