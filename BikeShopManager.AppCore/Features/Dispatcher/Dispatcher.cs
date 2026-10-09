using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.AppCore.Features.Dispatcher;

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public Dispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Result SendCommand<TCommand>(TCommand command)
        where TCommand : ICommandDefinition
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        //TODO : Implement validators with behavior pattern ???

        return handler.Handle(command);
    }

    public Result<TResult> SendCommand<TCommand, TResult>(TCommand command)
        where TCommand : ICommandDefinition<TResult>
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();

        //TODO : Implement validators with behavior pattern ???

        return handler.Handle(command);
    }

    public async Task<Result> SendCommandAsync<TCommand>(TCommand command)
        where TCommand : ICommandDefinition
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandlerAsync<TCommand>>();

        //TODO : Implement validators with behavior pattern ???

        return await handler.HandleAsync(command);
    }

    public async Task<Result<TResult>> SendCommandAsync<TCommand, TResult>(TCommand command)
        where TCommand : ICommandDefinition<TResult>
    {
        var handler = _serviceProvider.GetRequiredService<
            ICommandHandlerAsync<TCommand, TResult>
        >();

        //TODO : Implement validators with behavior pattern ???

        return await handler.HandleAsync(command);
    }

    public Result<TResult> SendQuery<TQuery, TResult>(TQuery query)
        where TQuery : IQueryDefinition<TResult>
    {
        var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        //TODO : Implement validators with behavior pattern ???

        return handler.Handle(query);
    }

    public async Task<Result<TResult>> SendQueryAsync<TQuery, TResult>(TQuery query)
        where TQuery : IQueryDefinition<TResult>
    {
        var handler = _serviceProvider.GetRequiredService<IQueryHandlerAsync<TQuery, TResult>>();

        //TODO : Implement validators with behavior pattern ???

        return await handler.HandleAsync(query);
    }
}
