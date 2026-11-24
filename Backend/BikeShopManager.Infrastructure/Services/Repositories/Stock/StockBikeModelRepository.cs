using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.Domain.Entities.Stock;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock;

public class StockBikeModelRepository : StockActivableRepositoryBase<BikeModel>
{
    private IDbSession _dbSession;

    public StockBikeModelRepository(IDbSession dbSession)
        : base(dbSession)
    {
        _dbSession = dbSession;
    }

    public override string GetTableName()
    {
        return "BikeModels";
    }

    public override async Task<int> AddAsync(BikeModel obj)
    {
        string sqlQuery =
            @$"INSERT INTO {GetTableCompleteName()} (Name, BrandId, Model, Description, CategoryId, PurchasePriceExclVat, SalePriceInclVat, VatRateId, EAN)
                OUTPUT INSERTED.ID
                VALUES(@Name, @BrandId, @Model, @Description, @CategoryId, @PurchasePriceExclVat, @SalePriceInclVat, @VatRateId, @EAN)";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Name", obj.Name);
        parameters.Add("@BrandId", obj.BrandId);
        parameters.Add("@Model", obj.Model);
        parameters.Add("@Description", obj.Description);
        parameters.Add("@CategoryId", obj.CategoryId);
        parameters.Add("@PurchasePriceExclVat", obj.PurchasePriceExclVat);
        parameters.Add("@SalePriceInclVat", obj.SalePriceInclVat);
        parameters.Add("@VatRateId", obj.VatRateId);
        parameters.Add("@EAN", obj.EAN);

        return await _dbSession.Connection.ExecuteScalarAsync<int>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<BikeModel?> GetAsync(int id)
    {
        string sqlQuery =
            @$"SELECT Id, Name, BrandId, Model, Description, CategoryId, PurchasePriceExclVat, SalePriceInclVat, VatRateId, EAN, IsActive, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName}.Id=@Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<BikeModel>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<BikeModel?> GetAsync(string name)
    {
        string sqlQuery =
            @$"SELECT Id, Name, BrandId, Model, Description, CategoryId, PurchasePriceExclVat, SalePriceInclVat, VatRateId, EAN, IsActive, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}
                WHERE {GetTableCompleteName}.Name=@Name";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Name", name);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<BikeModel>(
            sqlQuery,
            parameters,
            _dbSession.Transaction
        );
    }

    public override async Task<IEnumerable<BikeModel>> GetAsync()
    {
        string sqlQuery =
            @$"SELECT Id, Name, BrandId, Model, Description, CategoryId, PurchasePriceExclVat, SalePriceInclVat, VatRateId, EAN, IsActive, CreatedAt, UpdatedAt
                FROM {GetTableCompleteName()}";

        return await _dbSession.Connection.QueryAsync<BikeModel>(
            sqlQuery,
            transaction: _dbSession.Transaction
        );
    }
}
