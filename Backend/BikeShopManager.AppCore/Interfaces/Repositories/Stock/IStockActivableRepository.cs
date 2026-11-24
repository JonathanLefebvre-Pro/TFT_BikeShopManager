namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IStockActivableRepository<T>
{
    Task<bool> Disable(int id);
    Task<bool> Enable(int id);
}
