using System.Data;

namespace BikeShopManager.AppCore.Interfaces.Connection;

public interface IDbSession
{
    IDbConnection Connection { get; }
    IDbTransaction? Transaction { get; }

    TResult ExecuteInTransaction<TResult>(Func<TResult> action, Func<TResult, bool> isSuccess);

    Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<Task<TResult>> action,
        Func<TResult, bool> isSuccess
    );
}
