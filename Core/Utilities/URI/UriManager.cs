using Core.Entities.Concrete;
using Core.Utilities.URI;
using Microsoft.AspNetCore.WebUtilities;

namespace Core.Utilities.Uri
{
    /// <summary>
    /// URI manager
    /// </summary>
    public class UriManager : IUriService
    {
        /// <summary>
        /// Application url in launchSettings.json
        /// </summary>
        private readonly string _baseUri;

        public UriManager(string baseUri)
        {
            _baseUri = baseUri;
        }

        /// <summary>
        /// Get page uri from request
        /// </summary>
        /// <param name="filter">Pagination filter; page size, page number</param>
        /// <param name="route">API endpoint without base uri</param>
        /// <returns>Request URI with pagination</returns>
        public System.Uri GeneratePageRequestUri(PaginationFilter filter, string route)
        {
            var endpointUri = new System.Uri(string.Concat(_baseUri, route));
            var modifiedUri =
                QueryHelpers.AddQueryString(endpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new System.Uri(modifiedUri);
        }
    }
}