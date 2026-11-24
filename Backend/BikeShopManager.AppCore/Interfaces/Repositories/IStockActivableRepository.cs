namespace BikeShopManager.AppCore.Interfaces.Repositories;

public interface IStockActivableRepository
{
    Task<bool> Disable(int id);
    Task<bool> Enable(int id);
}
