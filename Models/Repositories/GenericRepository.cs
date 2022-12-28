using Microsoft.EntityFrameworkCore;
using Models.Context;
using System.Diagnostics.CodeAnalysis;

namespace Models.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly FluentValidationDbContext _context;

    public GenericRepository(FluentValidationDbContext context)
        => _context = context;

    [return: MaybeNull]
    public async Task<T?> Get(Guid id)
        => await _context.Set<T>().FindAsync(id);

    public async Task<IEnumerable<T>> GetAll()
        => await _context.Set<T>().ToListAsync();

    public async Task Add(T entity)
        => await _context.Set<T>().AddAsync(entity);

    public void Delete(T entity)
        => _context.Set<T>().Remove(entity);

    public void Update(T entity)
        => _context.Set<T>().Update(entity);

    public IAsyncEnumerable<T> AsAsyncEnumerable()
        => _context.Set<T>().AsAsyncEnumerable();
}

