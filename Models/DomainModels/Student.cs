using CSharpFunctionalExtensions;

namespace Models.DomainModels;

public class Student
{
    public Student()
    {
        Addresses = new List<Address>();
        StudentCourses = new List<StudentCourse>();
    }

    public Student(Guid id, FirstName firstName, string lastName, string nationalCode,
        Gender gender, string phone, string email, ICollection<Address> addresses, ICollection<StudentCourse> studentCourses)
        => (Id, FirstName, LastName, NationalCode, Gender, Phone, Email, Addresses, StudentCourses)
        = (id, firstName, lastName, nationalCode, gender, phone, email, addresses, studentCourses);

    public Guid Id { get; set; }
    public FirstName FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NationalCode { get; set; }
    public Gender Gender { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public int Age { get; set; }

    public ICollection<Address> Addresses { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; }
}


public class FirstName : ValueObject
{
    public string Value { get; }

    private FirstName(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}