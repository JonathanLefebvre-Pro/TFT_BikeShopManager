namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IStockActivableRepository<T>
    where T : class
{
    Task<bool> Disable(int id);
    Task<bool> Enable(int id);
}
