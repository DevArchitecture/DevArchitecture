using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using MongoDB.Bson;

namespace Core.DataAccess
{
    public interface IDocumentDbRepository<T>
        where T : DocumentDbEntity
    {
        void Add(T entity);
        IQueryable<T> GetList(Expression<Func<T, bool>> predicate = null);
        T GetById(ObjectId id);
        void AddMany(IEnumerable<T> entities);
        void Update(ObjectId id, T record);
        void Update(T record, Expression<Func<T, bool>> predicate);
        void Delete(ObjectId id);
        void Delete(T record);
        Task AddAsync(T entity);
        Task<IQueryable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> GetByIdAsync(ObjectId id);
        Task AddManyAsync(IEnumerable<T> entities);
        Task UpdateAsync(ObjectId id, T record);
        Task UpdateAsync(T record, Expression<Func<T, bool>> predicate);
        Task DeleteAsync(ObjectId id);
        Task DeleteAsync(T record);
        bool Any(Expression<Func<T, bool>> predicate = null);
    }
}