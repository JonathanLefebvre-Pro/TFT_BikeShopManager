using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Queries;

namespace BikeShopManager.AppCore.Features.Dispatcher;

public class TransactionalDispatcherDecorator(IDispatcher InnerDispatcher, IDbSession DbSession)
    : IDispatcher
{
    public Result SendCommand<TCommand>(TCommand command)
        where TCommand : ICommandDefinition
    {
        return DbSession.ExecuteInTransaction(
            () => InnerDispatcher.SendCommand(command),
            result => result.IsSuccess
        );
    }

    public Result<TResult> SendCommand<TCommand, TResult>(TCommand command)
        where TCommand : ICommandDefinition<TResult>
    {
        return DbSession.ExecuteInTransaction(
            () => InnerDispatcher.SendCommand<TCommand, TResult>(command),
            result => result.IsSuccess
        );
    }

    public async Task<Result> SendCommandAsync<TCommand>(TCommand command)
        where TCommand : ICommandDefinition
    {
        return await DbSession.ExecuteInTransactionAsync(
            () => InnerDispatcher.SendCommandAsync(command),
            result => result.IsSuccess
        );
    }

    public async Task<Result<TResult>> SendCommandAsync<TCommand, TResult>(TCommand command)
        where TCommand : ICommandDefinition<TResult>
    {
        return await DbSession.ExecuteInTransactionAsync(
            () => InnerDispatcher.SendCommandAsync<TCommand, TResult>(command),
            result => result.IsSuccess
        );
    }

    public Result<TResult> SendQuery<TQuery, TResult>(TQuery query)
        where TQuery : IQueryDefinition<TResult>
    {
        return InnerDispatcher.SendQuery<TQuery, TResult>(query);
    }

    public async Task<Result<TResult>> SendQueryAsync<TQuery, TResult>(TQuery query)
        where TQuery : IQueryDefinition<TResult>
    {
        return await InnerDispatcher.SendQueryAsync<TQuery, TResult>(query);
    }
}
