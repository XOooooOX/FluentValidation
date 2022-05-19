namespace Models.DomainModels;

public class Address
{
    public Address(Guid id, string name, string postalCode, Student student,
        string city, string state, string completeAddress, Guid studentId)
        => (Id, Name, PostalCode, City, State, CompleteAddress, StudentId, Student)
        = (id, name, postalCode, city, state, completeAddress, studentId, student);

    public Address()
        => Student = new Student();

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? CompleteAddress { get; set; }
    public Student Student { get; set; }

    public Guid StudentId { get; set; }
}

