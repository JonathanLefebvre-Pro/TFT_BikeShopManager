using BikeShopManager.WebApi.Dtos;
using FluentValidation;

namespace BikeShopManager.WebApi.Validators.AuthUser;

public class AuthUserUpdatePasswordDtoValidator : AbstractValidator<AuthUserUpdatePasswordDto>
{
    public AuthUserUpdatePasswordDtoValidator()
    {
        RuleFor(dto => dto.NewPassword)
        .Cascade(CascadeMode.Stop)
        .NotEmpty()
        .MinimumLength(5)
        .MaximumLength(100);

        RuleFor(dto => dto.OldPassword)
        .Cascade(CascadeMode.Stop)
        .NotEmpty()
        .MinimumLength(5)
        .MaximumLength(100);
    }
}
