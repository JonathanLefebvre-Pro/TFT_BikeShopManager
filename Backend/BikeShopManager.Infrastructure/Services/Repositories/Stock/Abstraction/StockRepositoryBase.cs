using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock.Abstract;

public abstract class StockRepositoryBase<T>(IDbSession DbSession) : IStockRepository<T>
    where T : class
{
    private const string _schemaName = @"Stock";

    public abstract Task<int> AddAsync(T obj);

    public virtual async Task<bool> DeleteAsync(int id)
    {
        string sqlQuery =
            @$"DELETE FROM {GetTableCompleteName()} WHERE {GetTableCompleteName()}.Id=@Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        int rows = await DbSession.Connection.ExecuteAsync(
            sqlQuery,
            parameters,
            DbSession.Transaction
        );

        return rows is not 0;
    }

    public abstract Task<T?> GetAsync(int id);
    public abstract Task<T?> GetAsync(string name);
    public abstract Task<IEnumerable<T>> GetAsync();

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
