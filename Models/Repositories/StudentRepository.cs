using Models.Context;
using Models.DomainModels;

namespace Models.Repositories;

public interface IStudentRepository : IGenericRepository<Student>
{
}

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(FluentValidationDbContext context) : base(context)
    {
    }
}

