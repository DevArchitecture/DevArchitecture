using Core.Entities;

namespace Entities.Concrete
{
    public class EntityExample : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}