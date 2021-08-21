using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.URI;

namespace Business.Helpers
{
    public static class PaginationHelper
    {
        /// <summary>
        /// Create paginated response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="paginationFilter"></param>
        /// <param name="totalRecords"></param>
        /// <param name="uriService"></param>
        /// <param name="route"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Paginated result</returns>
        public static PaginatedResult<IEnumerable<T>> CreatePaginatedResponse<T>(IEnumerable<T> data, PaginationFilter paginationFilter, int totalRecords, IUriService uriService, string route)
        {
            data = data.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize);
            int roundedTotalPages;
            var response =
                new PaginatedResult<IEnumerable<T>>(data, paginationFilter.PageNumber, paginationFilter.PageSize);
            var totalPages = totalRecords / (double)paginationFilter.PageSize;
            if (paginationFilter.PageNumber <= 0 || paginationFilter.PageSize <= 0)
            {
                roundedTotalPages = 1;
                paginationFilter.PageNumber = 1;
                paginationFilter.PageSize = 1;
            }
            else
            {
                roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            }

            response.NextPage = paginationFilter.PageNumber >= 1 && paginationFilter.PageNumber < roundedTotalPages
                ? uriService.GeneratePageRequestUri(
                    new PaginationFilter(paginationFilter.PageNumber + 1, paginationFilter.PageSize), route)
                : null;
            response.PreviousPage =
                paginationFilter.PageNumber - 1 >= 1 && paginationFilter.PageNumber <= roundedTotalPages
                    ? uriService.GeneratePageRequestUri(
                        new PaginationFilter(paginationFilter.PageNumber - 1, paginationFilter.PageSize), route)
                    : null;
            response.FirstPage =
                uriService.GeneratePageRequestUri(new PaginationFilter(1, paginationFilter.PageSize), route);
            response.LastPage =
                uriService.GeneratePageRequestUri(new PaginationFilter(roundedTotalPages, paginationFilter.PageSize), route);
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;
        }
    }
}