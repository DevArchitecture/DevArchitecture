namespace Core.Utilities.URI
{
    using Core.Entities.Concrete;

    public interface IUriService
    {
        System.Uri GeneratePageRequestUri(PaginationFilter filter, string route);
    }
}