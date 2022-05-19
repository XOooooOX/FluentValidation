namespace Models.DomainModels;

public class Course
{
    public Course()
        => StudentCourses = new List<StudentCourse>();

    public Course(Guid id, string name, ICollection<StudentCourse> studentCourses)
        => (Id, Name, StudentCourses)
        = (id, name, studentCourses);

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; }
}

