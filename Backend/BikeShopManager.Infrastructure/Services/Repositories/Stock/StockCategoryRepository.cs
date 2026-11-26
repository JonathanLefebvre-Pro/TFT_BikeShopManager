using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.Infrastructure.Services.Repositories.Stock.Abstract;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock;

public class StockCategoryRepository : StockRepositoryBase<Category>
{
    private IDbSession _dbSession;

    public StockCategoryRepository(IDbSession dbSession)
        : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public override string GetTableName()
    {
        return "Categories";
    }

    public override async Task<int> AddAsync(Category obj)
    {
        string sqlQuery =
            @$"INSERT INTO {GetTableCompleteName()} (Name, Description, ParentCategoryId, Domain)
                OUTPUT INSERTED.ID
                VALUES(@Name, @Description, @ParentCategoryId, @Domain)";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Name", obj.Name);
        parameters.Add("@Description", obj.Description);
        parameters.Add("@ParentCategoryId", obj.ParentCategoryId);
        parameters.Add("@Domain", obj.Domain);

        return await _dbSession.Connection.ExecuteScalarAsync<int>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<Category?> GetAsync(int id)
    {
        string sqlQuery =
            @$"SELECT Id, Name, Description, ParentCategoryId, Domain, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName()}.Id=@Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Category>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<Category?> GetAsync(string name)
    {
        string sqlQuery =
            @$"SELECT Id, Name, Description, ParentCategoryId, Domain, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName()}.Name=@Name";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Name", name);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Category>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<IEnumerable<Category>> GetAsync()
    {
        string sqlQuery =
            @$"SELECT Id, Name, Description, ParentCategoryId, Domain, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}";

        return await _dbSession.Connection.QueryAsync<Category>(
            sqlQuery,
            transaction: _dbSession.Transaction
        );
    }

    public override async Task<bool> UpdateAsync(Category obj)
    {
        throw new NotImplementedException();
    }
}
