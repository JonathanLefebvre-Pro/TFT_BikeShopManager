using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.Infrastructure.Services.Repositories.Stock.Abstract;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock;

public class StockVatRateRepository : StockRepositoryBase<VatRate>
{
    private IDbSession _dbSession;

    public StockVatRateRepository(IDbSession dbSession)
        : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public override string GetTableName()
    {
        return "VatRates";
    }

    public override async Task<int> AddAsync(VatRate obj)
    {
        string sqlQuery =
            @$"INSERT INTO {GetTableCompleteName()} (Label, Rate)
                OUTPUT INSERTED.ID
                VALUES(@Label, @Rate)";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Label", obj.Label);
        parameters.Add("@Rate", obj.Rate);

        return await _dbSession.Connection.ExecuteScalarAsync<int>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<VatRate?> GetAsync(int id)
    {
        string sqlQuery =
            @$"SELECT Id, Label, Rate, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName()}.Id=@Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<VatRate>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<VatRate?> GetAsync(string label)
    {
        string sqlQuery =
            @$"SELECT Id, Label, Rate, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName()}.Label=@Label";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Label", label);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<VatRate>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<IEnumerable<VatRate>> GetAsync()
    {
        string sqlQuery =
            @$"SELECT Id, Label, Rate, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}";

        return await _dbSession.Connection.QueryAsync<VatRate>(
            sqlQuery,
            transaction: _dbSession.Transaction
        );
    }

    public override async Task<bool> UpdateAsync(VatRate obj)
    {
        throw new NotImplementedException();
    }
}
