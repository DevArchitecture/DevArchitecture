namespace Core.Entities.Concrete
{
    public class Language:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
