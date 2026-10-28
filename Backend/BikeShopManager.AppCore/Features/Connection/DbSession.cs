using System.Data;
using BikeShopManager.AppCore.Interfaces.Connection;

namespace BikeShopManager.AppCore.Features.Connection;

public class DbSession : IDbSession
{
    private readonly IDbConnection _connection;

    public DbSession(IDbConnection connection)
    {
        _connection = connection;
    }

    public IDbTransaction? Transaction { get; private set; }

    public IDbConnection Connection
    {
        get
        {
            if (_connection.State is ConnectionState.Closed)
                _connection.Open();

            return _connection;
        }
    }

    public TResult ExecuteInTransaction<TResult>(
        Func<TResult> action,
        Func<TResult, bool> isSuccess
    )
    {
        if (Transaction is not null)
            return action();

        using var transaction = Connection.BeginTransaction();
        Transaction = transaction;

        try
        {
            var result = action();

            if (isSuccess(result))
                transaction.Commit();
            else
                transaction.Rollback();

            return result;
        }
        finally
        {
            Transaction = null;
        }
    }

    public async Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<Task<TResult>> action,
        Func<TResult, bool> isSuccess
    )
    {
        if (Transaction is not null)
            return await action();

        using var transaction = Connection.BeginTransaction();
        Transaction = transaction;

        try
        {
            var result = await action();

            if (isSuccess(result))
                transaction.Commit();
            else
                transaction.Rollback();

            return result;
        }
        finally
        {
            Transaction = null;
        }
    }
}
