using System.Data;
using BikeShopManager.AppCore.Features.Commands;
using BikeShopManager.AppCore.Features.Dispatcher;
using BikeShopManager.AppCore.Features.Queries;
using BikeShopManager.AppCore.Interfaces.Commands;
using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.AppCore.Interfaces.Repositories;
using BikeShopManager.Domain.Entities;
using BikeShopManager.Infrastructure.Services;
using BikeShopManager.Infrastructure.Services.Customers;
using BikeShopManager.Infrastructure.Services.Employees;
using Microsoft.Data.SqlClient;
using Scalar.AspNetCore;

string conStringTFT = @"GOS-VDI406\TFTIC";
string conStringJo = @"JONATHAN\DATAVIZ";
string connectionString =
    @"Data Source="
    + conStringJo
    + @";Initial Catalog=TFT_BikeShopManager_WebApi;Integrated Security=True;Trust Server Certificate=True";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(connectionString));
builder.Services.AddScoped<ICustomerRepository, CustomerService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeService>();

builder.Services.AddScoped<IDispatcher, Dispatcher>();
builder.Services.AddScoped<
    ICommandHandlerAsync<AddCustomerCommand, int>,
    AddCustomerCommandHandler
>();
builder.Services.AddScoped<
    ICommandHandlerAsync<UpdateCustomerCommand>,
    UpdateCustomerCommandHandler
>();
builder.Services.AddScoped<
    ICommandHandlerAsync<EnableCustomerCommand>,
    EnableCustomerCommandHandler
>();
builder.Services.AddScoped<
    ICommandHandlerAsync<DisableCustomerCommand>,
    DisableCustomerCommandHandler
>();
builder.Services.AddScoped<
    IQueryHandlerAsync<GetCustomerByIdQuery, Customer>,
    GetCustomerByIdQueryHandler
>();

builder.Services.AddScoped<
    ICommandHandlerAsync<AddEmployeeCommand, int>,
    AddEmployeeCommandHandler
>();
builder.Services.AddScoped<
    ICommandHandlerAsync<UpdateEmployeeCommand>,
    UpdateEmployeeCommandHandler
>();
builder.Services.AddScoped<
    ICommandHandlerAsync<EnableEmployeeCommand>,
    EnableEmployeeCommandHandler
>();
builder.Services.AddScoped<
    ICommandHandlerAsync<DisableEmployeeCommand>,
    DisableEmployeeCommandHandler
>();
builder.Services.AddScoped<
    IQueryHandlerAsync<GetEmployeeByIdQuery, Employee>,
    GetEmployeeByIdQueryHandler
>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
