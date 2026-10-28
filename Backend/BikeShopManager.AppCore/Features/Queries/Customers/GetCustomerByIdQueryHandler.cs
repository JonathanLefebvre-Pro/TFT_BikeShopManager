using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Customers;

public class GetCustomerByIdQueryHandler : IQueryHandlerAsync<GetCustomerByIdQuery, Customer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;

    public GetCustomerByIdQueryHandler(
        ICustomerRepository customerRepository,
        IUserRepository userRepository
    )
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<Customer>> HandleAsync(GetCustomerByIdQuery query)
    {
        Customer? customer = await _customerRepository.GetCustomerById(query.Id);

        if (customer is null)
            return Result<Customer>.Failure(CustomerError.NotFound);

        User? user = await _userRepository.GetUserById(customer.UserId);

        if (user is null)
            return Result<Customer>.Failure(UserError.NotFound);

        customer.User = user;
        return customer;
    }
}
