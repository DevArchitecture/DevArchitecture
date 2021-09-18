namespace Core.Entities.Concrete
{
    public class GeneralFilter : IPagingFilter
    {
        public int Page { get; set; }
        public string PropertyName { get; set; }
        public bool Asc { get; set; }
    }
}
