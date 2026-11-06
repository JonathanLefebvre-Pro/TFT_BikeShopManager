using BikeShopManager.AppCore.Features.Commands.Employees;
using BikeShopManager.AppCore.Features.Options;
using BikeShopManager.AppCore.Features.Results;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.Domain.Entities;
using Microsoft.Extensions.Options;

namespace BikeShopManager.WebApi.Seeders;

public sealed class AdminSeederHostedService(
    IServiceScopeFactory ServiceScopeFactory,
    IOptions<AdminSeederOptions> Options
) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //Get admin values from options in appsettings.json
        AdminSeederOptions seed = Options.Value;
        if (string.IsNullOrWhiteSpace(seed.Login) || string.IsNullOrWhiteSpace(seed.Password))
            return;

        using IServiceScope scope = ServiceScopeFactory.CreateScope();
        //Get dispatcher service
        IDispatcher dispatcher = scope.ServiceProvider.GetRequiredService<IDispatcher>();
        //Create a new user with the admin values
        User user = new User
        {
            FirstName = seed.FirstName,
            LastName = seed.LastName,
            Email = seed.Login,
        };
        //Create a new employee with the admin values
        Employee employee = new Employee
        {
            Password = seed.Password,
            HiredAt = DateOnly.FromDateTime(DateTime.UtcNow),
            IsActive = true,
        };

        Result<int> result = await dispatcher.SendCommandAsync<AddEmployeeCommand, int>(
            new AddEmployeeCommand(employee, user, seed.RoleId)
        );
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
