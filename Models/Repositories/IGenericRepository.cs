using System.Diagnostics.CodeAnalysis;

namespace Models.Repositories;

public interface IGenericRepository<T> where T : class
{
    [return: MaybeNull]
    Task<T?> Get(Guid id);
    Task<IEnumerable<T>> GetAll();
    Task Add(T entity);
    void Delete(T entity);
    void Update(T entity);
}

