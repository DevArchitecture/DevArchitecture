namespace Core.Entities
{
    public class BaseEntity : BaseEntity<int> { }
    public class BaseEntity<T> : IEntity
    {
        public virtual T Id { get; set; }
    }
}
