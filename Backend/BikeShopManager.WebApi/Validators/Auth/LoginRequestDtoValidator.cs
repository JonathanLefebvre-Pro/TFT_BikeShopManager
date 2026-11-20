using BikeShopManager.WebApi.Dtos.Auth;
using FluentValidation;

namespace BikeShopManager.WebApi.Validators.Auth;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(dto => dto.Login)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("The {PropertyName} is requireddddddd.")
        .EmailAddress()
        .MinimumLength(6)
        .MaximumLength(254);

        RuleFor(dto => dto.Password)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("The {PropertyName} is requiredddddd.")
        .MinimumLength(5)
        .MaximumLength(100);
    }
}
