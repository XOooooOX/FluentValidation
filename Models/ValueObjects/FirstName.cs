using CSharpFunctionalExtensions;
using System.Diagnostics.CodeAnalysis;

namespace Models.ValueObjects;

public class FirstName : ValueObject
{
    public string Value { get; }

    private FirstName(string value)
        => Value = value;

    public static Result<FirstName, Error> Create([MaybeNull] string? input)
    {
        string firstName = input!.Trim();

        if (firstName.Length < 5)
            return Errors.Student.FirstNameMinimumCharacterControl(5);

        if (firstName.Length > 30)
            return Errors.Student.FirstNameMaximumCharacterControl(30);

        return (new FirstName(firstName));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

