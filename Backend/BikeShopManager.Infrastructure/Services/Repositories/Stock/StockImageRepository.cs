using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.Infrastructure.Services.Repositories.Stock.Abstract;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock;

public class StockImageRepository : StockRepositoryBase<Image>
{
    private IDbSession _dbSession;

    public StockImageRepository(IDbSession dbSession)
        : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public override string GetTableName()
    {
        return "Images";
    }

    public override async Task<int> AddAsync(Image obj)
    {
        string sqlQuery =
            @$"INSERT INTO {GetTableCompleteName()} (ArticleId, BikeModelId, Url, ImageName)
                OUTPUT INSERTED.ID
                VALUES(@ArticleId, @BikeModelId, @Url, @ImageName)";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@ArticleId", obj.ArticleId);
        parameters.Add("@BikeModelId", obj.BikeModelId);
        parameters.Add("@Url", obj.Url);
        parameters.Add("@ImageName", obj.ImageName);

        return await _dbSession.Connection.ExecuteScalarAsync<int>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<Image?> GetAsync(int id)
    {
        string sqlQuery =
            @$"SELECT Id, ArticleId, BikeModelId, Url, ImageName, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName()}.Id=@Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Image>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<Image?> GetAsync(string imageName)
    {
        string sqlQuery =
            @$"SELECT Id, ArticleId, BikeModelId, Url, ImageName, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName()}.ImageName=@ImageName";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@ImageName", imageName);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Image>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<IEnumerable<Image>> GetAsync()
    {
        string sqlQuery =
            @$"SELECT Id, ArticleId, BikeModelId, Url, ImageName, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}";

        return await _dbSession.Connection.QueryAsync<Image>(
            sqlQuery,
            transaction: _dbSession.Transaction
        );
    }
}
