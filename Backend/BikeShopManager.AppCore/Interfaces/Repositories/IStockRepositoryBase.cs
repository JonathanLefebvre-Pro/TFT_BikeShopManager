namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IStockRepositoryBase<T>
{
    Task<T?> GetAsync(int id);
    Task<T?> GetAsync(string name);
    Task<IEnumerable<T>> GetAsync();
    Task<int> AddAsync(T obj);
    Task<bool> DeleteAsync(int id);
}
