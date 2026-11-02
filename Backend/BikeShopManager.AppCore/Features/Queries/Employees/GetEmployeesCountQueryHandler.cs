using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;

namespace BikeShopManager.AppCore.Features.Queries.Employees;

public class GetEmployeesCountQueryHandler(IEmployeeRepository EmployeeRepository)
    : IQueryHandlerAsync<GetEmployeesCountQuery, int>
{
    public async Task<Result<int>> HandleAsync(GetEmployeesCountQuery query)
    {
        int count = await EmployeeRepository.GetEmployeesCount();
        return Result<int>.Success(count);
    }
}
