using Models.Context;

namespace Models.Repositories;

public interface IUnitOfWork : IDisposable
{
    IStudentRepository StudentRepository { get; }
    ICourseRepository CourseRepository { get; }
    int Complete();
}
public class UnitOfWork : IUnitOfWork
{
    private readonly FluentValidationDbContext _context;
    private bool _disposedValue;

    public IStudentRepository StudentRepository { get; }
    public ICourseRepository CourseRepository { get; }

    public UnitOfWork(FluentValidationDbContext context,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository)
    {
        _context = context;
        StudentRepository = studentRepository;
        CourseRepository = courseRepository;
    }

    public int Complete()
       => _context.SaveChanges();

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
                _context.Dispose();

            _disposedValue = true;
        }
    }

    ~UnitOfWork()
        => Dispose(disposing: false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

