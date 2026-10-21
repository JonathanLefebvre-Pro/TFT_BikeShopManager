using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services.Customers;

public class GetCustomersQueryHandler
    : IQueryHandlerAsync<GetCustomersQuery, PageResponse<Customer>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;

    public GetCustomersQueryHandler(
        ICustomerRepository customerRepository,
        IUserRepository userRepository
    )
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<PageResponse<Customer>>> HandleAsync(GetCustomersQuery query)
    {
        int pageNumber = Math.Max(1, query.PageNumber);
        int pageSize = Math.Clamp(query.PageSize, 1, 50);

        int offset = (pageNumber - 1) * pageSize;
        int limit = pageSize;

        IEnumerable<Customer> customers = await _customerRepository.GetCustomers(offset, limit);
        int totalRecords = await _customerRepository.GetCustomersCount();

        if (customers is null || customers.Count() <= 0)
            return Result<PageResponse<Customer>>.Failure(CustomerError.NotFound);

        foreach (Customer customer in customers)
        {
            //TODO : Cancel query if one customer has no user ???? This could be a problem - other solution ?
            User? user = await _userRepository.GetUserById(customer.UserId);
            //if (user is null)
            //return Result<IEnumerable<Customer>>.Failure(UserError.NotFound);
            if (user is not null)
                customer.User = user;
        }

        PageResponse<Customer> response = new PageResponse<Customer>()
        {
            Data = customers,
            PageSize = pageSize,
            PageNumber = pageNumber,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize),
        };

        return Result<PageResponse<Customer>>.Success(response);
    }
}
