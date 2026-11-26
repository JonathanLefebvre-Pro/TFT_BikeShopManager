using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.Infrastructure.Services.Repositories.Stock.Abstract;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock;

public class StockBrandRepository : StockRepositoryBase<Brand>
{
    private IDbSession _dbSession;

    public StockBrandRepository(IDbSession dbSession)
        : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public override string GetTableName()
    {
        return "Brands";
    }

    public override async Task<int> AddAsync(Brand obj)
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

    public override async Task<Brand?> GetAsync(int id)
    {
        string sqlQuery =
            @$"SELECT Id, Name, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName()}.Id=@Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Brand>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<Brand?> GetAsync(string name)
    {
        string sqlQuery =
            @$"SELECT Id, Name, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName()}.Name=@name";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Name", name);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Brand>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<IEnumerable<Brand>> GetAsync()
    {
        string sqlQuery =
            @$"SELECT Id, Name, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}";

        return await _dbSession.Connection.QueryAsync<Brand>(
            sqlQuery,
            transaction: _dbSession.Transaction
        );
    }

    public override async Task<bool> UpdateAsync(Brand obj)
    {
        throw new NotImplementedException();
    }
}
