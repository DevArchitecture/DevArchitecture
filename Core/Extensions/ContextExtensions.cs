using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Core.Extensions
{
    public static class ContextExtensions
    {
    /// <summary>
    ///Finds the Set of the given type from within the given Db context and returns a query object cast to the requested type.  
    /// The given T type must be implemented by the object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_context"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static DbSet<T> Set<T>(this DbContext _context, Type t) where T : class
        {
            return (DbSet<T>)_context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
        }

    /// <summary>
    /// Returns the DbSet object as a queryable of the desired type (T).
    /// 
    /// Here the object attached to DbContext should implement the T type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_context"></param>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public static IQueryable<T> QueryableOf<T>(this DbContext _context, string typeName) where T : class
        {
            var type = _context.Model.GetEntityTypes(typeName).First();          
            var q = (IQueryable)_context
                .GetType()
                .GetMethod("Set")
                .MakeGenericMethod(type.ClrType)
                .Invoke(_context, null);
            return q.OfType<T>();
        }
    }
}
