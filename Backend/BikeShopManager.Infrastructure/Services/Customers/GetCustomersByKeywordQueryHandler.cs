using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services.Customers;

public class GetCustomersByKeywordQueryHandler
    : IQueryHandlerAsync<GetCustomersByKeywordQuery, IEnumerable<Customer>>
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

    public async Task<Result<IEnumerable<Customer>>> HandleAsync(GetCustomersByKeywordQuery query)
    {
        IEnumerable<User> users = await _userRepository.GetUsersByKeyword(query.keyword);
        if (users is null || users.Count() <= 0)
            return Result<IEnumerable<Customer>>.Failure(UserError.NotFound);

        //TODO : For each users, get his customer
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
            return Result<IEnumerable<Customer>>.Failure(CustomerError.NotFound);

        return Result<IEnumerable<Customer>>.Success(customers);
    }
}
