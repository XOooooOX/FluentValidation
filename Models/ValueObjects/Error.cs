using CSharpFunctionalExtensions;
using System.Diagnostics.CodeAnalysis;

namespace Models.ValueObjects;

public sealed class Error : ValueObject
{
    private const string Seprator = "||";
    public string Code { get; init; }
    public string Message { get; init; }

    internal Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }

    public string Serialize()
        => $"{Code}{Seprator}{Message}";

    public static Error Deserialize(string serialize)
    {
        string[] data = serialize.Split(new[] { Seprator }, StringSplitOptions.RemoveEmptyEntries);

        if (data.Length < 2)
            throw new Exception($"Invalid Error Serialization: '{serialize}'");

        return new Error(data[0], data[1]);
    }
}

public static class Errors
{
    public static class General
    {
        public static Error NotFound([MaybeNull] long? id = null)
        {
            string forId = id == null ? "" : $"For Id{id}";
            return new Error("1", $"Record Not Found {forId}");
        }

        public static Error ValueIsInvalid()
            => new Error("2", "Value Is Invalid");

        public static Error InternalServerError(string error)
            => new Error("500", error);

        public static Error ValueIsRequired()
            => new Error("10", "Value Is Required");

        public static Error InvalidLength()
            => new Error("15", "Length is Invalid");
    }

    public static class Student
    {
        public static Error EmailIsTaken([MaybeNull] string? email)
        {
            string forEmail = email == null ? "" : $"For Id{email}";
            return new Error("3", $"Email {forEmail} Is Taken");
        }

        public static Error NationalCodeIsTaken()
            => new Error("3", $"NationalCode Is Taken");

        public static Error FirstNameMinimumCharacterControl([MaybeNull] int? min = 0)
            => new Error("4", $"First Name Must Be More Than {min} Character");

        public static Error FirstNameMaximumCharacterControl([MaybeNull] int? max = 0)
            => new Error("5", $"First Name Must Be Less Than {max} Character");
    }
}