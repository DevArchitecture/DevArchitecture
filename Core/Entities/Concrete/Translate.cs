

namespace Core.Entities.Concrete
{
    public class Translate : IEntity
    {
        public int Id { get; set; }
        public int LangId { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
