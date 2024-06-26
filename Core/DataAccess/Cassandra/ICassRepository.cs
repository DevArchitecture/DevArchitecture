using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.DataAccess.Cassandra;

public interface ICassRepository<T> where T : class, IEntity
{
    void Add(T entity);

    IQueryable<T> GetList(Expression<Func<T, bool>> predicate = null);

    Task UpdateAsync(T record);

    void Update(T record);
    Task DeleteAsync(T record);

    void Delete(T record);

    T GetById(long id);

    Task AddAsync(T entity);

    Task<IQueryable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null);

    Task<T> GetByIdAsync(long id);

    Task<T> GetAsync(Expression<Func<T, bool>> predicate);

    bool Any(Expression<Func<T, bool>> predicate = null);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);

    long GetCount(Expression<Func<T, bool>> predicate = null);
    Task<long> GetCountAsync(Expression<Func<T, bool>> predicate = null);
}