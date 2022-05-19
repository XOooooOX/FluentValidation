using Models.Context;
using Models.DomainModels;

namespace Models.Repositories;

public interface ICourseRepository : IGenericRepository<Course>
{
}

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(FluentValidationDbContext context) : base(context)
    {
    }
}

