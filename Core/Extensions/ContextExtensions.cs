using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core.Extensions
{
    public static class ContextExtensions
    {
        /// <summary>
        /// Finds the Set of the given type from within the given Db context and returns a query object cast to the requested type.
        /// The given T type must be implemented by the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static DbSet<T> Set<T>(this DbContext context, Type t)
            where T : class
        {
            return (DbSet<T>)context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(context, null);
        }

        /// <summary>
        /// Returns the DbSet object as a queryable of the desired type (T).
        ///
        /// Here the object attached to DbContext should implement the T type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static IQueryable<T> QueryableOf<T>(this DbContext context, string typeName)
            where T : class
        {
            var type = context.Model.FindEntityType(typeName);
            var q = (IQueryable)context
                .GetType()
                .GetMethod("Set")
                .MakeGenericMethod(type.ClrType)
                .Invoke(context, null);
            return q.OfType<T>();
        }
    }
}