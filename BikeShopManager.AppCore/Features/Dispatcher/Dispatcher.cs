using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BikeShopManager.AppCore.Features.Dispatcher;

public class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IDbSession _session;

    public Dispatcher(IServiceProvider serviceProvider, IDbSession session)
    {
        _serviceProvider = serviceProvider;
        _session = session;
    }

    public Result SendCommand<TCommand>(TCommand command)
        where TCommand : ICommandDefinition
    {
        return _session.ExecuteInTransaction(
            () => _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>().Handle(command),
            result => result.IsSuccess
        );
    }

    public Result<TResult> SendCommand<TCommand, TResult>(TCommand command)
        where TCommand : ICommandDefinition<TResult>
    {
        return _session.ExecuteInTransaction(
            () =>
                _serviceProvider
                    .GetRequiredService<ICommandHandler<TCommand, TResult>>()
                    .Handle(command),
            result => result.IsSuccess
        );
    }

    public async Task<Result> SendCommandAsync<TCommand>(TCommand command)
        where TCommand : ICommandDefinition
    {
        return await _session.ExecuteInTransactionAsync(
            () =>
                _serviceProvider
                    .GetRequiredService<ICommandHandlerAsync<TCommand>>()
                    .HandleAsync(command),
            result => result.IsSuccess
        );
    }

    public async Task<Result<TResult>> SendCommandAsync<TCommand, TResult>(TCommand command)
        where TCommand : ICommandDefinition<TResult>
    {
        return await _session.ExecuteInTransactionAsync(
            () =>
                _serviceProvider
                    .GetRequiredService<ICommandHandlerAsync<TCommand, TResult>>()
                    .HandleAsync(command),
            result => result.IsSuccess
        );
    }

    public Result<TResult> SendQuery<TQuery, TResult>(TQuery query)
        where TQuery : IQueryDefinition<TResult>
    {
        return _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>().Handle(query);
    }

    public async Task<Result<TResult>> SendQueryAsync<TQuery, TResult>(TQuery query)
        where TQuery : IQueryDefinition<TResult>
    {
        return await _serviceProvider
            .GetRequiredService<IQueryHandlerAsync<TQuery, TResult>>()
            .HandleAsync(query);
    }
}
