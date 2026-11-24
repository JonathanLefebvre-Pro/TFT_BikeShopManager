using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock.Abstract;

public abstract class StockActivableRepositoryBase<T>
    : StockRepositoryBase<T>,
        IStockActivableRepository<T>
    where T : class
{
    private IDbSession _dbSession;

    protected StockActivableRepositoryBase(IDbSession dbSession)
        : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public virtual async Task<bool> Disable(int id)
    {
        string sqlQuery = @$"UPDATE {GetTableCompleteName()} SET IsActive=@IsActive WHERE Id=@Id";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@IsActive", false);

        int rows = await _dbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );

        return rows is not 0;
    }

    public virtual async Task<bool> Enable(int id)
    {
        string sqlQuery = @$"UPDATE {GetTableCompleteName()} SET IsActive=@IsActive WHERE Id=@Id";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@IsActive", true);

        int rows = await _dbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );

        return rows is not 0;
    }
}
