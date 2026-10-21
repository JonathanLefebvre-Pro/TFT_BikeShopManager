using BikeShopManager.AppCore.Interfaces.Connection;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using Dapper;

namespace BikeShopManager.Infrastructure.Services.Repositories;

public class UserRepository(IDbSession DbSession) : IUserRepository
{
    private const string _tableName = @"Persons.Users";

    public async Task<int> CreateUser(User user)
    {
        string insertUserSqlQuery =
            @$"INSERT INTO {_tableName} (FirstName, LastName, Email, PhoneNumber, Address, City, ZipCode, Country, BirthDate, AllowMarketing) 
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

        return await DbSession.Connection.ExecuteScalarAsync<int>(
            insertUserSqlQuery,
            insertUserParameters,
            DbSession.Transaction
        );
    }

    public async Task<User?> GetUserById(int id)
    {
        string userSqlQuery =
            @$"SELECT Id, FirstName, LastName , Email , PhoneNumber , Address , City , ZipCode , Country , BirthDate , AllowMarketing, CreatedAt, UpdatedAt
                FROM {_tableName} WHERE {_tableName}.Id = @UserId";
        DynamicParameters userParameters = new DynamicParameters();
        userParameters.Add("@UserId", id);

        return await DbSession.Connection.QuerySingleOrDefaultAsync<User>(
            userSqlQuery,
            userParameters,
            DbSession.Transaction
        );
    }

    public async Task<ExistingUser?> GetUserByMail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        string getUserSqlQuery =
            @$"SELECT Id, FirstName, LastName, Email, BirthDate 
                    FROM {_tableName}
                    WHERE Email=@Email";
        DynamicParameters getUserParameters = new DynamicParameters();
        getUserParameters.Add("@Email", email);

        return await DbSession.Connection.QuerySingleOrDefaultAsync<ExistingUser>(
            getUserSqlQuery,
            getUserParameters,
            DbSession.Transaction
        );
    }

    public async Task<IEnumerable<User>> GetUsersByKeyword(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return Enumerable.Empty<User>();

        string getUserSqlQuery = @$"SELECT Id, ";

        string[] headers =
        [
            "FirstName",
            "LastName",
            "Email",
            "PhoneNumber",
            "Address",
            "City",
            "ZipCode",
            "Country",
            "BirthDate",
        ];
        for (int i = 0; i < headers.Length; i++)
            getUserSqlQuery += headers[i] + ", ";

        getUserSqlQuery += @$"AllowMarketing, CreatedAt, UpdatedAt FROM {_tableName} WHERE ";

        for (int i = 0; i < headers.Length; i++)
        {
            if (i == 0)
            {
                getUserSqlQuery += headers[i] + @" LIKE @Keyword ";
                continue;
            }

            getUserSqlQuery += @"OR " + headers[i] + @" LIKE @Keyword ";
        }

        DynamicParameters getUserParameters = new DynamicParameters();
        getUserParameters.Add("@Keyword", "%" + keyword + "%");

        return await DbSession.Connection.QueryAsync<User>(
            getUserSqlQuery,
            getUserParameters,
            DbSession.Transaction
        );
    }

    public async Task<IEnumerable<User>> GetUsersByKeywords(
        int offset,
        int limit,
        string[] keywords
    )
    {
        if (keywords is null || keywords.Length is 0)
            return Enumerable.Empty<User>();

        string getUserSqlQuery = @$"SELECT Id, ";

        string[] headers =
        [
            "FirstName",
            "LastName",
            "Email",
            "PhoneNumber",
            "Address",
            "City",
            "ZipCode",
            "Country",
            "BirthDate",
        ];
        for (int i = 0; i < headers.Length; i++)
            getUserSqlQuery += headers[i] + ", ";

        getUserSqlQuery +=
            @$"AllowMarketing, CreatedAt, UpdatedAt 
                FROM {_tableName}
                WHERE (";

        for (int k = 0; k < keywords.Length; k++)
        {
            if (k > 0)
                getUserSqlQuery += @"AND (";

            for (int i = 0; i < headers.Length; i++)
            {
                if (i == 0)
                {
                    getUserSqlQuery += headers[i] + @$" LIKE @Keyword{k} ";
                    continue;
                }

                getUserSqlQuery += @"OR " + headers[i] + @$" LIKE @Keyword{k} ";

                if (i == headers.Length - 1)
                    getUserSqlQuery += @")";
            }
        }

        getUserSqlQuery +=
            @$"ORDER BY Id
            OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";

        DynamicParameters getUserParameters = new DynamicParameters();
        getUserParameters.Add("@Offset", offset);
        getUserParameters.Add("@Limit", limit);
        for (int k = 0; k < keywords.Length; k++)
            getUserParameters.Add(@$"@Keyword{k}", "%" + keywords[k] + "%");

        return await DbSession.Connection.QueryAsync<User>(
            getUserSqlQuery,
            getUserParameters,
            DbSession.Transaction
        );
    }

    public async Task<int> GetUsersCountByKeywords(string[] keywords)
    {
        if (keywords is null || keywords.Length is 0)
            return 0;

        string getUserSqlQuery = @$"SELECT COUNT(*) ";

        string[] headers =
        [
            "FirstName",
            "LastName",
            "Email",
            "PhoneNumber",
            "Address",
            "City",
            "ZipCode",
            "Country",
            "BirthDate",
        ];

        getUserSqlQuery +=
            @$"FROM {_tableName}
                WHERE (";

        for (int k = 0; k < keywords.Length; k++)
        {
            if (k > 0)
                getUserSqlQuery += @"AND (";

            for (int i = 0; i < headers.Length; i++)
            {
                if (i == 0)
                {
                    getUserSqlQuery += headers[i] + @$" LIKE @Keyword{k} ";
                    continue;
                }

                getUserSqlQuery += @"OR " + headers[i] + @$" LIKE @Keyword{k} ";

                if (i == headers.Length - 1)
                    getUserSqlQuery += @")";
            }
        }

        DynamicParameters getUserParameters = new DynamicParameters();
        for (int k = 0; k < keywords.Length; k++)
            getUserParameters.Add(@$"@Keyword{k}", "%" + keywords[k] + "%");

        return await DbSession.Connection.ExecuteScalarAsync<int>(
            getUserSqlQuery,
            getUserParameters,
            DbSession.Transaction
        );
    }

    public async Task<bool> IsUserExist(int id)
    {
        string userSqlQuery = @$"SELECT Id FROM {_tableName} WHERE {_tableName}.Id = @UserId";
        DynamicParameters userParameters = new DynamicParameters();
        userParameters.Add("@UserId", id);

        int userId = await DbSession.Connection.QuerySingleOrDefaultAsync<int>(
            userSqlQuery,
            userParameters,
            DbSession.Transaction
        );

        return userId is not 0;
    }

    public async Task<bool> UpdateUser(int id, User user)
    {
        string userSqlQuery =
            @$"UPDATE {_tableName} 
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
                WHERE {_tableName}.Id = @Id";

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

        int rows = await DbSession.Connection.ExecuteAsync(
            userSqlQuery,
            userParameters,
            DbSession.Transaction
        );

        return rows is not 0;
    }
}
