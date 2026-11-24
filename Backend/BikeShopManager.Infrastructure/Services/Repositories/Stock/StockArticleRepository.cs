using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.Infrastructure.Services.Repositories.Stock.Abstract;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock;

public class StockArticleRepository : StockActivableRepositoryBase<Article>
{
    private IDbSession _dbSession;

    public StockArticleRepository(IDbSession dbSession)
        : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public override string GetTableName()
    {
        return "Articles";
    }

    public override async Task<int> AddAsync(Article obj)
    {
        string sqlQuery =
            @$"INSERT INTO {GetTableCompleteName()} (Name, BrandId, Description, CategoryId, PurchasePriceExclVat, SalePriceInclVat, VatRateId, AvailableQuantity, MinQuantity, MaxQuantity, EAN)
                OUTPUT INSERTED.ID
                VALUES(@Name, @BrandId, @Description, @CategoryId, @PurchasePriceExclVat, @SalePriceInclVat, @VatRateId, @AvailableQuantity, @MinQuantity, @MaxQuantity, @EAN)";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Name", obj.Name);
        parameters.Add("@BrandId", obj.BrandId);
        parameters.Add("@Description", obj.Description);
        parameters.Add("@CategoryId", obj.CategoryId);
        parameters.Add("@PurchasePriceExclVat", obj.PurchasePriceExclVat);
        parameters.Add("@SalePriceInclVat", obj.SalePriceInclVat);
        parameters.Add("@VatRateId", obj.VatRateId);
        parameters.Add("@AvailableQuantity", obj.AvailableQuantity);
        parameters.Add("@MinQuantity", obj.MinQuantity);
        parameters.Add("@MaxQuantity", obj.MaxQuantity);
        parameters.Add("@EAN", obj.EAN);

        return await _dbSession.Connection.ExecuteScalarAsync<int>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<Article?> GetAsync(int id)
    {
        string sqlQuery =
            @$"SELECT Id, Name, BrandId, Description, CategoryId, PurchasePriceExclVat, SalePriceInclVat, VatRateId, AvailableQuantity, MinQuantity, MaxQuantity, EAN, IsActive, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName}.Id=@Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Article>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<Article?> GetAsync(string name)
    {
        string sqlQuery =
            @$"SELECT Id, Name, BrandId, Description, CategoryId, PurchasePriceExclVat, SalePriceInclVat, VatRateId, AvailableQuantity, MinQuantity, MaxQuantity, EAN, IsActive, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName}.Name=@Name";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Name", name);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<Article>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<IEnumerable<Article>> GetAsync()
    {
        string sqlQuery =
            @$"SELECT Id, Name, BrandId, Description, CategoryId, PurchasePriceExclVat, SalePriceInclVat, VatRateId, AvailableQuantity, MinQuantity, MaxQuantity, EAN, IsActive, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}";

        return await _dbSession.Connection.QueryAsync<Article>(
            sqlQuery,
            transaction: _dbSession.Transaction
        );
    }
}
