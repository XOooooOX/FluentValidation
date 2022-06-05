using Models.Context;
using Models.DomainModels;
using System.Diagnostics.CodeAnalysis;

namespace Models.Repositories;

public interface IStudentRepository : IGenericRepository<Student>
{
    [return: MaybeNull]
    bool? ExistByNationalCode([MaybeNull] string? nationalCode);
}

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    protected new readonly FluentValidationDbContext _context;

    public StudentRepository([NotNull] FluentValidationDbContext context) : base(context)
        => _context = context;

    [return: MaybeNull]
    public bool? ExistByNationalCode([MaybeNull] string? nationalCode)
        => _context?.Student?.Any(o => o.NationalCode == nationalCode);
}