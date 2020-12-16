using Core.DataAccess;
using Core.Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    /// <summary>
    /// Bu sýnýf Repository pattern kullanýr IEntityRepository'den implemente edilir T generic tipinde nesne alýr.
    /// sýnýfa özel operasyonlar bu sýnýf içinde tanýmlanýr. örneðini IUserRepository interface i içinde bulabilirsiniz.
    /// </summary>
    public interface IUserRepository : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(int userId);
    }
}