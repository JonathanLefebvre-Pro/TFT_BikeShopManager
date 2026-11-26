using BikeShopManager.Domain.Entities.Stock;

namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IStockRepository<TEntity>
    where TEntity : StockEntityBase
{
    Task<TEntity?> GetAsync(int id);
    Task<TEntity?> GetAsync(string name);
    Task<IEnumerable<TEntity>> GetAsync();
    Task<int> AddAsync(TEntity obj);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(int id, TEntity obj);
}
