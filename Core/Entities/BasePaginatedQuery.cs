using System;
using Core.Utilities.Results;
using MediatR;

namespace Core.Entities
{
    public abstract class BasePaginatedQuery<T> : IRequest<IDataResult<PaginatedResult<System.Collections.Generic.IEnumerable<T>>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        protected BasePaginatedQuery(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 10 : Math.Min(pageSize, 100);
        }
    }
}
