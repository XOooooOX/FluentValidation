using System.Diagnostics.CodeAnalysis;

namespace Models.DomainModels;

public class Address
{
    public Address([NotNull] Guid id, string? name, string? postalCode,
        string? city, string? state, string? completeAddress)
        => (Id, Name, PostalCode, City, State, CompleteAddress)
        = (id, name, postalCode, city, state, completeAddress);

    public Address()
        => Student = new Student();

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? CompleteAddress { get; set; }
    public Student? Student { get; set; }

    public Guid StudentId { get; set; }
}

