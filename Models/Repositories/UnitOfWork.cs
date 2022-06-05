using Models.Context;
using System.Diagnostics.CodeAnalysis;

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

    public IStudentRepository StudentRepository { get; init; }
    public ICourseRepository CourseRepository { get; init; }

    public UnitOfWork(FluentValidationDbContext context,
        [NotNull] IStudentRepository studentRepository,
        [NotNull] ICourseRepository courseRepository)
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

