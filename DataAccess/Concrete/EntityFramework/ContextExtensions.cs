using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public static class ContextExtensions
    {
        /// <summary>
        /// Verilen Db context içinden verilen türün Set'ini bulur ve
        /// istenen türe cast edilen bir sorgu nesnesi döner.
        /// 
        /// Verilen T türü nesne tarafından implement edilmiş olmalıdır.
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
        /// DbSet nesnesini istenen cinsten (T) bir queryable olarak döner.
        /// 
        /// Burada DbContext'e eklenmiş olan nesne T türünü implement etmelidir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_context"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static IQueryable<T> QueryableOf<T>(this DbContext _context, string typeName) where T : class
        {
            var type = _context.Model.GetEntityTypes(typeName).First();
            // once modelden gercek type'i coz
            var q = (IQueryable)_context
                .GetType()
                .GetMethod("Set")
                .MakeGenericMethod(type.ClrType)
                .Invoke(_context, null);
            return q.OfType<T>();
        }
    }
}
