using BikeShopManager.AppCore.Interfaces.Repositories.Stock;
using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.Infrastructure.Services.Repositories.Stock;

public class StockVatRateRepository : IStockVatRateRepository
{
    public Task<int> Add(VatRate obj)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<VatRate> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<VatRate> Get(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VatRate>> Get()
    {
        throw new NotImplementedException();
    }
}
