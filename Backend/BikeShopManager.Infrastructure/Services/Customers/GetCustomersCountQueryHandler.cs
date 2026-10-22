using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.Infrastructure.Services.Customers;

public class GetCustomersCountQueryHandler(ICustomerRepository CustomerRepository)
    : IQueryHandlerAsync<GetCustomersCountQuery, int>
{
    public async Task<Result<int>> HandleAsync(GetCustomersCountQuery query)
    {
        int count = await CustomerRepository.GetCustomersCount();
        return Result<int>.Success(count);
    }
}
