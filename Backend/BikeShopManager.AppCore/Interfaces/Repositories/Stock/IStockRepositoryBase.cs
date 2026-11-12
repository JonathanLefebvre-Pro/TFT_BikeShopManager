namespace BikeShopManager.AppCore.Interfaces.Repositories.Stock;

public interface IStockRepositoryBase<T>
{
    Task<T> Get(int id);
    Task<T> Get(string name);
    Task<IEnumerable<T>> Get();
    Task<int> Add(T obj);
    Task<bool> Delete(int id);
}
