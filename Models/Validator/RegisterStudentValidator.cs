using FluentValidation;
using Models.ViewModels;

namespace Models.Validator;

public class RegisterStudentValidator : AbstractValidator<RegisterStudent>
{
    public RegisterStudentValidator()
    {
        RuleFor(o => o.NationalCode)
            .NotNull().WithMessage("لطفا کد ملی را وارد کنید")
            .Length(10).WithMessage("تعداد کاراکترها صحیح نیست");

        RuleFor(o => o.FirstName)
            .NotEmpty();

        RuleFor(o => o.RegisterAddress).NotNull();

        RuleForEach(o => o.RegisterAddress)
            .SetValidator(new RegisterAddressValidator());

        RuleFor(o => o.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(o => o.Age)
            .GreaterThan(18)
            .LessThan(40);

    }
}

public class RegisterAddressValidator : AbstractValidator<RegisterAddress>
{
    public RegisterAddressValidator()
    {
        RuleFor(o => o.City).NotEmpty();
        RuleFor(o => o.State).NotEmpty();
        RuleFor(o => o.PostalCode).NotEmpty();
        RuleFor(o => o.Name).NotEmpty();
        RuleFor(o => o.CompleteAddress).NotEmpty();
    }
}

