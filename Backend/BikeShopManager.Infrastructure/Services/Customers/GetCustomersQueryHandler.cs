using System.Text.RegularExpressions;
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

        if (query.Keywords == null || query.Keywords.Length is 0)
        {
            int totalRecords = await _customerRepository.GetCustomersCount();
            IEnumerable<Customer> customers = await _customerRepository.GetCustomers(offset, limit);

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
        else
        {
            string keyword = Regex.Replace(query.Keywords.Trim(), @"\s+", " ");
            string[] keywords = keyword.Split(' ');

            int totalRecords = await _userRepository.GetUsersCount(keywords);
            IEnumerable<User> users = await _userRepository.GetUsers(offset, limit, keywords);
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
}
