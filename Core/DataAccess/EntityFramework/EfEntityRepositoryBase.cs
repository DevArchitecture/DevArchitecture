﻿using Core.Entities;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework;

/// <summary>
///
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TContext"></typeparam>
public class EfEntityRepositoryBase<TEntity, TContext>
    : IEntityRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext
{
    public EfEntityRepositoryBase(TContext context)
    {
        Context = context;
    }

    protected TContext Context { get; }

    public TEntity Add(TEntity entity)
    {
        return Context.Add(entity).Entity;
    }

    public TEntity Update(TEntity entity)
    {
        Context.Update(entity);
        return entity;
    }

    public void Delete(TEntity entity)
    {
        Context.Remove(entity);
    }

    public TEntity Get(Expression<Func<TEntity, bool>> expression)
    {
        return Context.Set<TEntity>().FirstOrDefault(expression);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Context.Set<TEntity>().AsQueryable().FirstOrDefaultAsync(expression);
    }

    public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression = null)
    {
        return expression == null
            ? Context.Set<TEntity>().AsNoTracking()
            : Context.Set<TEntity>().Where(expression).AsNoTracking();
    }

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
    {
        return expression == null
            ? await Context.Set<TEntity>().ToListAsync()
            : await Context.Set<TEntity>().Where(expression).ToListAsync();
    }

    //sources: https://www.nuget.org/packages/Apsiyon  |||  https://github.com/vmutlu/ApsiyonFramework
    public PagingResult<TEntity> GetListForPaging(int page, string propertyName, bool asc, Expression<Func<TEntity, bool>> expression = null, params Expression<Func<TEntity, object>>[] includeEntities)
    {
        var list = Context.Set<TEntity>().AsQueryable();

        if (includeEntities.Length > 0)
        {
            list = list.IncludeMultiple(includeEntities);
        }

        if (expression != null)
        {
            list = list.Where(expression).AsQueryable();
        }

        list = asc ? list.AscOrDescOrder(ESort.ASC, propertyName) : list.AscOrDescOrder(ESort.DESC, propertyName);
        int totalCount = list.Count();

        var start = (page - 1) * 10;
        list = list.Skip(start).Take(10);

        return new PagingResult<TEntity>(list.ToList(), totalCount, true, $"{totalCount} records listed.");
    }


    public int SaveChanges()
    {
        return Context.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }

    public IQueryable<TEntity> Query()
    {
        return Context.Set<TEntity>();
    }

    public Task<int> Execute(FormattableString interpolatedQueryString)
    {
        return Context.Database.ExecuteSqlInterpolatedAsync(interpolatedQueryString);
    }

    /// <summary>
    /// Transactional operations is prohibited when working with InMemoryDb!
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="action"></param>
    /// <param name="successAction"></param>
    /// <param name="exceptionAction"></param>
    /// <returns></returns>
    public TResult InTransaction<TResult>(Func<TResult> action, Action successAction = null, Action<Exception> exceptionAction = null)
    {
        var result = default(TResult);
        try
        {
            if (Context.Database.ProviderName.EndsWith("InMemory"))
            {
                result = action();
                SaveChanges();
            }
            else
            {
                using var tx = Context.Database.BeginTransaction();
                try
                {
                    result = action();
                    SaveChanges();
                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                    throw;
                }
            }

            successAction?.Invoke();
        }
        catch (Exception ex)
        {
            if (exceptionAction == null)
            {
                throw;
            }

            exceptionAction(ex);
        }

        return result;
    }

    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression = null)
    {
        if (expression == null)
        {
            return await Context.Set<TEntity>().CountAsync();
        }
        else
        {
            return await Context.Set<TEntity>().CountAsync(expression);
        }
    }

    public int GetCount(Expression<Func<TEntity, bool>> expression = null)
    {
        return expression == null ? Context.Set<TEntity>().Count() : Context.Set<TEntity>().Count(expression);
    }
}
