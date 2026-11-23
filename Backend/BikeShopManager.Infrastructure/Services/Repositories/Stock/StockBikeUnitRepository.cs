using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.Domain.Entities.Stock;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock;

public class StockBikeUnitRepository : StockBaseRepository<BikeUnit>
{
    private IDbSession _dbSession;

    public StockBikeUnitRepository(IDbSession dbSession)
        : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public override string GetTableName()
    {
        return "BikeUnits";
    }

    public override async Task<int> AddAsync(BikeUnit obj)
    {
        string sqlQuery =
            @$"INSERT INTO {GetTableCompleteName()} (BikeModelId, SerialNumber, LocationId, Status)
                OUTPUT INSERTED.ID
                VALUES(@BikeModelId, @SerialNumber, @LocationId, @Status)";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@BikeModelId", obj.BikeModelId);
        parameters.Add("@SerialNumber", obj.SerialNumber);
        parameters.Add("@LocationId", obj.LocationId);
        parameters.Add("@Status", obj.Status);

        return await _dbSession.Connection.ExecuteScalarAsync<int>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<BikeUnit?> GetAsync(int id)
    {
        string sqlQuery =
            @$"SELECT Id, BikeModelId, SerialNumber, LocationId, Status, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName}.Id=@Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<BikeUnit>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<BikeUnit?> GetAsync(string serialNumber)
    {
        string sqlQuery =
            @$"SELECT Id, BikeModelId, SerialNumber, LocationId, Status, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName}.SerialNumber=@SerialNumber";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@SerialNumber", serialNumber);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<BikeUnit>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<IEnumerable<BikeUnit>> GetAsync()
    {
        string sqlQuery =
            @$"SELECT Id, BikeModelId, SerialNumber, LocationId, Status, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}";

        return await _dbSession.Connection.QueryAsync<BikeUnit>(
            sqlQuery,
            transaction: _dbSession.Transaction
        );
    }
}
