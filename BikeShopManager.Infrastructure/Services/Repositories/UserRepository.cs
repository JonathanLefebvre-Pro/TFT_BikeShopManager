using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbSession _dbSession;

    public UserRepository(IDbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<int> CreateUser(User user)
    {
        string insertUserSqlQuery =
            @"INSERT INTO Persons.Users (FirstName, LastName, Email, PhoneNumber, Address, City, ZipCode, Country, BirthDate, AllowMarketing) 
                    OUTPUT INSERTED.ID
                    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address, @City, @ZipCode, @Country, @BirthDate, @AllowMarketing)";
        DynamicParameters insertUserParameters = new DynamicParameters();
        insertUserParameters.Add("@FirstName", user.FirstName);
        insertUserParameters.Add("@LastName", user.LastName);
        insertUserParameters.Add("@Email", user.Email);
        insertUserParameters.Add("@PhoneNumber", user.PhoneNumber);
        insertUserParameters.Add("@Address", user.Address);
        insertUserParameters.Add("@City", user.City);
        insertUserParameters.Add("@ZipCode", user.ZipCode);
        insertUserParameters.Add("@Country", user.Country);
        insertUserParameters.Add("@BirthDate", user.BirthDate);
        insertUserParameters.Add("@AllowMarketing", user.AllowMarketing);

        return await _dbSession.Connection.ExecuteScalarAsync<int>(
            insertUserSqlQuery,
            insertUserParameters,
            _dbSession.Transaction
        );
    }

    public async Task<User?> GetUserById(int id)
    {
        string userSqlQuery =
            @"SELECT Id, FirstName, LastName , Email , PhoneNumber , Address , City , ZipCode , Country , BirthDate , AllowMarketing, CreatedAt, UpdatedAt
                FROM Persons.Users WHERE Persons.Users.Id = @UserId";
        DynamicParameters userParameters = new DynamicParameters();
        userParameters.Add("@UserId", id);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<User>(
            userSqlQuery,
            userParameters,
            _dbSession.Transaction
        );
    }

    public async Task<ExistingUser?> GetUserByMail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        string getUserSqlQuery =
            @"SELECT Id, FirstName, LastName, Email, BirthDate 
                    FROM Persons.Users
                    WHERE Email=@Email";
        DynamicParameters getUserParameters = new DynamicParameters();
        getUserParameters.Add("@Email", email);

        return await _dbSession.Connection.QuerySingleOrDefaultAsync<ExistingUser>(
            getUserSqlQuery,
            getUserParameters,
            _dbSession.Transaction
        );
    }

    public async Task<bool> UpdateUser(int id, User user)
    {
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
        userParameters.Add("@Id", id);
        userParameters.Add("@FirstName", user.FirstName);
        userParameters.Add("@LastName", user.LastName);
        userParameters.Add("@Email", user.Email);
        userParameters.Add("@PhoneNumber", user.PhoneNumber);
        userParameters.Add("@Address", user.Address);
        userParameters.Add("@City", user.City);

        userParameters.Add("@ZipCode", user.ZipCode);
        userParameters.Add("@Country", user.Country);
        userParameters.Add("@BirthDate", user.BirthDate);
        userParameters.Add("@AllowMarketing", user.AllowMarketing);
        userParameters.Add("@UpdatedAt", DateTime.UtcNow);

        int rows = await _dbSession.Connection.ExecuteAsync(
            userSqlQuery,
            userParameters,
            _dbSession.Transaction
        );

        return rows is not 0;
    }
}
