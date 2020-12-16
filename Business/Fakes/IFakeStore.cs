using System.Collections.Generic;

namespace Business.Fakes
{
    public interface IFakeStore
    {
        List<TEntity> Set<TEntity>();
    }
}