using System.Data;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Queries;

namespace BikeShopManager.AppCore.Interfaces.Dispatcher;

public delegate Task<TResult> RequestHandlerDelegate<TResult>(IDbTransaction? transact = default);

public interface IDispatcher
{
    Result SendCommand<TCommand>(TCommand command)
        where TCommand : ICommandDefinition;

    Result<TResult> SendCommand<TCommand, TResult>(TCommand command)
        where TCommand : ICommandDefinition<TResult>;

    Task<Result> SendCommandAsync<TCommand>(TCommand command)
        where TCommand : ICommandDefinition;

    Task<Result<TResult>> SendCommandAsync<TCommand, TResult>(TCommand command)
        where TCommand : ICommandDefinition<TResult>;

    Result<TResult> SendQuery<TQuery, TResult>(TQuery query)
        where TQuery : IQueryDefinition<TResult>;

    Task<Result<TResult>> SendQueryAsync<TQuery, TResult>(TQuery query)
        where TQuery : IQueryDefinition<TResult>;
}
