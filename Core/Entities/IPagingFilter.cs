namespace Core.Entities
{
    public interface IPagingFilter
    {
        int Page { get; set; }
        string PropertyName { get; set; }
        bool Asc { get; set; }
    }
}
