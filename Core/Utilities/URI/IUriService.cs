using Core.Entities.Concrete;

namespace Core.Utilities.URI
{
    public interface IUriService
    {
        System.Uri GeneratePageRequestUri(PaginationFilter filter, string route);
    }
}