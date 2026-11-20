using BikeShopManager.WebApi.Dtos;
using FluentValidation;

namespace BikeShopManager.WebApi.Validators.Customers;

public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateDto>
{
    public CustomerUpdateDtoValidator()
    {
        RuleFor(dto => dto.VatNumber)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(20)
            .Matches(@"^[A-Z]{2}[A-Z0-9+*]{2,}$");

        RuleFor(dto => dto.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100)
            .Matches(@"^(?=.{2,100}$)\p{L}+(?:[ '’-]\p{L}+)*$");

        RuleFor(dto => dto.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100)
            .Matches(@"^(?=.{2,100}$)\p{L}+(?:[ '’-]\p{L}+)*$");

        RuleFor(dto => dto.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(254)
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$");

        RuleFor(dto => dto.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(20)
            .Matches(@"^(?:(?:\+|00)32|0)\s?(?:[1-9]\d?)(?:[\s./-]?\d{2,3}){3,4}$");

        RuleFor(dto => dto.Address).Cascade(CascadeMode.Stop).MaximumLength(200);

        RuleFor(dto => dto.City)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(50)
            .Matches(@"^\p{L}+\.?(?:[ '’\-]\p{L}+\.?)*$");

        RuleFor(dto => dto.ZipCode)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(10)
            .Matches(@"^[A-Z0-9]+(?:[ -][A-Z0-9]+)*$");

        RuleFor(dto => dto.Country)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(50)
            .Matches(@"^\p{L}+(?:[ '’\-.]\p{L}+)*$");

        RuleFor(dto => dto.BirthDate).Cascade(CascadeMode.Stop).Must(BeValidDate);
    }

    private bool BeValidDate(DateOnly? birthdate)
    {
        return birthdate <= DateOnly.FromDateTime(DateTime.Today);
    }
}
