using BikeShopManager.WebApi.Dtos.Auth;
using FluentValidation;

namespace BikeShopManager.WebApi.Validators.Auth;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(dto => dto.Login)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("The {PropertyName} is required.")
        .EmailAddress()
        .MinimumLength(6)
        .MaximumLength(254)
        .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$");

        RuleFor(dto => dto.Password)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("The {PropertyName} is required.")
        .MinimumLength(5)
        .MaximumLength(100);
    }
}
