using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.Domain.Entities.Stock;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock;

public class StockLocationRepository : StockBaseRepository<Location>
{
    private IDbSession _dbSession;

    public StockLocationRepository(IDbSession dbSession)
        : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public override string GetTableName()
    {
        return "Locations";
    }

    public override async Task<int> AddAsync(Location obj)
    {
        string sqlQuery =
            @$"INSERT INTO {GetTableCompleteName()} (Name)
                OUTPUT INSERTED.ID
                VALUES(@Name)";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Name", obj.Name);

        return await _dbSession.Connection.ExecuteScalarAsync<int>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<Location?> GetAsync(int id)
    {
        string sqlQuery =
            @$"SELECT Id, Name, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName}.Id=@Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Location>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<Location?> GetAsync(string name)
    {
        string sqlQuery =
            @$"SELECT Id, Name, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName}.Name=@Name";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Name", name);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Location>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<IEnumerable<Location>> GetAsync()
    {
        string sqlQuery =
            @$"SELECT Id, Name, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}";

        return await _dbSession.Connection.QueryAsync<Location>(
            sqlQuery,
            transaction: _dbSession.Transaction
        );
    }
}
