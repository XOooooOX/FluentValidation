namespace Models.DomainModels;

public class Student
{
    public Student()
    {
        Addresses = new List<Address>();
        StudentCourses = new List<StudentCourse>();
    }

    public Student(Guid id, string firstName, string lastName, string nationalCode,
        Gender gender, string phone, string email, ICollection<Address> addresses, ICollection<StudentCourse> studentCourses)
        => (Id, FirstName, LastName, NationalCode, Gender, Phone, Email, Addresses, StudentCourses)
        = (id, firstName, lastName, nationalCode, gender, phone, email, addresses, studentCourses);

    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NationalCode { get; set; }
    public Gender Gender { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public ICollection<Address> Addresses { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; }
}

public enum Gender
{
    Male = 1,
    Female,
    Unspecified
}

