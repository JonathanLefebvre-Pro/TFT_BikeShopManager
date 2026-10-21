using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services.Customers;

public class GetCustomersByKeywordQueryHandler
    : IQueryHandlerAsync<GetCustomersByKeywordsQuery, PageResponse<Customer>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;

    public GetCustomersByKeywordQueryHandler(
        ICustomerRepository customerRepository,
        IUserRepository userRepository
    )
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<PageResponse<Customer>>> HandleAsync(GetCustomersByKeywordsQuery query)
    {
        int pageNumber = Math.Max(1, query.PageNumber);
        int pageSize = Math.Clamp(query.PageSize, 1, 50);

        int offset = (pageNumber - 1) * pageSize;
        int limit = pageSize;

        int totalRecords = await _userRepository.GetUsersCount(query.Keywords);
        IEnumerable<User> users = await _userRepository.GetUsers(offset, limit, query.Keywords);
        if (users is null || users.Count() <= 0)
            return Result<PageResponse<Customer>>.Failure(UserError.NotFound);

        //For each users, get his customer
        List<Customer> customers = new List<Customer>();
        foreach (User user in users)
        {
            Customer? customer = await _customerRepository.GetCustomerByUserId(user.Id);
            if (customer is not null)
            {
                customer.User = user;
                customers.Add(customer);
            }
        }

        if (customers.Count() is 0)
            return Result<PageResponse<Customer>>.Failure(CustomerError.NotFound);

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
