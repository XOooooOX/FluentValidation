namespace Models.DomainModels;

public class Course
{
    public Course()
        => StudentCourses = new List<StudentCourse>();

    public Course(Guid id, string name, string? description,
        ICollection<StudentCourse> studentCourses)
        => (Id, Name, StudentCourses, Description)
        = (id, name, studentCourses, description);

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public ICollection<StudentCourse> StudentCourses { get; set; }
}

