using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities.Stock;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock.Abstract;

public abstract class StockRepositoryBase<TEntity> : IStockRepository<TEntity>
    where TEntity : StockEntityBase
{
    private IDbSession _dbSession;

    protected StockRepositoryBase(IDbSession dbSession)
    {
        _dbSession = dbSession;
    }

    private const string _schemaName = @"Stock";

    public abstract Task<int> AddAsync(TEntity obj);

    public virtual async Task<bool> DeleteAsync(int id)
    {
        string sqlQuery =
            @$"DELETE FROM {GetTableCompleteName()} WHERE {GetTableCompleteName()}.Id=@Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        int rows = await _dbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );

        return rows is not 0;
    }

    public abstract Task<TEntity?> GetAsync(int id);
    public abstract Task<TEntity?> GetAsync(string name);
    public abstract Task<IEnumerable<TEntity>> GetAsync();
    public abstract Task<bool> UpdateAsync(TEntity obj);

    public string GetSchemaName()
    {
        return _schemaName;
    }

    public string GetTableCompleteName()
    {
        return $"{_schemaName}.{GetTableName()}";
    }

    public abstract string GetTableName();
}
