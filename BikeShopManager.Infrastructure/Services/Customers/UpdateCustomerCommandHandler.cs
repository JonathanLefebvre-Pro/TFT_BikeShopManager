using System;
using System.Data;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Errors;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services;

public class UpdateCustomerCommandHandler : ICommandHandlerAsync<UpdateCustomerCommand>
{
    private readonly IDbConnection _dbConnection;
    private readonly IDispatcher _dispatcher;

    public UpdateCustomerCommandHandler(IDbConnection DbConnection, IDispatcher dispatcher)
    {
        _dbConnection = DbConnection;
        _dispatcher = dispatcher;
        _dbConnection.Open();
    }

    public async Task<Result> HandleAsync(
        UpdateCustomerCommand command,
        IDbTransaction transact = null!
    )
    {
        using (
            IDbTransaction transaction = transact is not null
                ? transact
                : _dbConnection.BeginTransaction()
        )
        {
            //Get the customer based on the command Id parameter
            //Get the user row linked to the customer UserId
            Result<Customer> result = await _dispatcher.SendQueryAsync<
                GetCustomerByIdQuery,
                Customer
            >(
                new GetCustomerByIdQuery(command.Id) /*,
                transaction*/
            );
            if (result.IsFailure)
            {
                transaction.Rollback();
                return Result.Failure(CustomerError.NotFound);
            }

            //Update the user row
            string userSqlQuery =
                @"UPDATE Persons.Users 
                SET FirstName=@FirstName, 
                LastName=@LastName, 
                Email=@Email, 
                PhoneNumber=@PhoneNumber, 
                Address=@Address, 
                City=@City, 
                ZipCode=@ZipCode, 
                Country=@Country, 
                BirthDate=@BirthDate, 
                AllowMarketing=@AllowMarketing, 
                UpdatedAt=@UpdatedAt 
                WHERE Persons.Users.Id = @Id";

            DynamicParameters userParameters = new DynamicParameters();
            userParameters.Add("@Id", result.Data.UserId);
            userParameters.Add("@FirstName", command.User.FirstName);
            userParameters.Add("@LastName", command.User.LastName);
            userParameters.Add("@Email", command.User.Email);
            userParameters.Add("@PhoneNumber", command.User.PhoneNumber);
            userParameters.Add("@Address", command.User.Address);
            userParameters.Add("@City", command.User.City);

            userParameters.Add("@ZipCode", command.User.ZipCode);
            userParameters.Add("@Country", command.User.Country);
            userParameters.Add("@BirthDate", command.User.BirthDate);
            userParameters.Add("@AllowMarketing", command.User.AllowMarketing);
            userParameters.Add("@UpdatedAt", DateTime.UtcNow);

            int affectedRows = 0;
            try
            {
                affectedRows = await _dbConnection.ExecuteAsync(
                    userSqlQuery,
                    userParameters,
                    transaction
                );
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex;
            }

            if (affectedRows is 0)
            {
                transaction.Rollback();
                return Result.Failure(CustomerError.NotUpdated);
            }

            //Update the customer row
            string customerSqlQuery =
                @"UPDATE Persons.Customers 
                SET VatNumber=@VatNumber, UpdatedAt=@UpdatedAt 
                WHERE Persons.Customers.Id=@Id";
            DynamicParameters customerParameters = new DynamicParameters();
            customerParameters.Add("@Id", command.Id);
            customerParameters.Add("@VatNumber", command.Customer.VatNumber);
            customerParameters.Add("@UpdatedAt", DateTime.UtcNow);

            try
            {
                affectedRows = await _dbConnection.ExecuteAsync(
                    customerSqlQuery,
                    customerParameters,
                    transaction
                );
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex;
            }

            if (affectedRows is 0)
            {
                transaction.Rollback();
                return Result.Failure(CustomerError.NotUpdated);
            }

            transaction.Commit();
            return Result.Success();
        }
    }
}
