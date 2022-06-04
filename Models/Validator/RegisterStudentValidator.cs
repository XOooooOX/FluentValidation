using FluentValidation;
using Models.ViewModels;

namespace Models.Validator;

public class RegisterStudentValidator : AbstractValidator<RegisterStudent>
{
    public RegisterStudentValidator()
    {
        //RuleSet(ActionCrud.Add.ToString(), () =>
        //{
        //    //RuleFor(o => o.Phone).NotEmpty().Matches(@"^0(9\d{9})$");
        //});

        //CascadeMode = CascadeMode.Stop;

        RuleFor(o => o.NationalCode)/*.Cascade(CascadeMode.Stop)*/
            .NotNull().WithMessage("لطفا کد ملی را وارد کنید")
            .Length(10).WithMessage("تعداد کاراکترها صحیح نیست");


        //RuleFor(o => o.FirstName)
        //    .NotEmpty()
        //    .Must(o => o != null && o.StartsWith("h") && o.EndsWith("a"));

        //RuleFor(o => o.LastName)
        //    .Must(o => o != null && o.StartsWith("h") && o.EndsWith("a"))
        //    .NotEmpty();

        //RuleFor(o => o.FirstName)
        //    .StartAndEndControl("h", "a");

        RuleFor(o => o.LastName)
            .StartAndEndControl("z", "b");

        RuleFor(o => o.RegisterAddress).NotNull()
            .Must(o => o?.Count > 0 && o.Count <= 2);

        RuleForEach(o => o.RegisterAddress)
            .SetValidator(new RegisterAddressValidator());

        //RuleFor(o => o.Email)
        //    .NotEmpty()
        //    .EmailAddress();

        RuleFor(o => o.Age)
            .GreaterThan(18)
            .LessThan(40);


        When(o => !string.IsNullOrEmpty(o.Phone), () =>
        {
            RuleFor(o => o.Phone).NotEmpty().Matches(@"^0(9\d{9})$");

            RuleFor(o => o.Email).Null();

        }).Otherwise(() =>
        {
            RuleFor(o => o.Email).NotEmpty().EmailAddress();

            RuleFor(o => o.Phone).Null();
        });

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


public static class CustomeValidator
{
    public static IRuleBuilder<T, string?> StartAndEndControl<T>
        (this IRuleBuilder<T, string?> ruleBuilder, string? start = null, string? end = null)
        => ruleBuilder.Custom((input, Context) =>
        {
            if (start != null && input != null && !input.StartsWith(start))
                Context.AddFailure($"Must Start With {start}, But You Enter {input[0]}");

            if (end != null && input != null && !input.EndsWith(end))
                Context.AddFailure($"Must End With {end}, But You Enter {input[^1]}");
        });
}