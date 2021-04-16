namespace Business.Fakes
{
    using System.Collections.Generic;

    public interface IFakeStore
    {
        List<TEntity> Set<TEntity>();
    }
}