using BikeShopManager.WebApi.Dtos;
using FluentValidation;

namespace BikeShopManager.WebApi.Validators.Employees;

public class EmployeeAddDtoValidator : AbstractValidator<EmployeeAddDto>
{
    public EmployeeAddDtoValidator() { }
}
