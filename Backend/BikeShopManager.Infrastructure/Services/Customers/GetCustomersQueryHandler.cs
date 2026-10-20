using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.Infrastructure.Services.Customers;

public class GetCustomersQueryHandler : IQueryHandlerAsync<GetCustomersQuery, IEnumerable<Customer>>
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

    public async Task<Result<IEnumerable<Customer>>> HandleAsync(GetCustomersQuery query)
    {
        IEnumerable<Customer> customers = await _customerRepository.GetCustomers(
            query.Offset,
            query.Next
        );

        if (customers is null || customers.Count() <= 0)
            return Result<IEnumerable<Customer>>.Failure(CustomerError.NotFound);

        foreach (Customer customer in customers)
        {
            //TODO : Cancel query if one customer has no user ???? This could be a problem - other solution ?
            User? user = await _userRepository.GetUserById(customer.UserId);
            //if (user is null)
            //return Result<IEnumerable<Customer>>.Failure(UserError.NotFound);
            if (user is not null)
                customer.User = user;
        }

        return Result<IEnumerable<Customer>>.Success(customers);
    }
}
