using Core.Utilities.Messages;

namespace Core.Utilities.Results
{
    public class PaginatedResult<T> : IDataResult<T>
    {
        public PaginatedResult(T data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 1 : pageSize;
            Data = data;
            Message = PaginationMessages.ListPaged;
            Success = true;
        }

        public bool Success { get; set; }
        public string Message { get; }
        public T Data { get; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public System.Uri FirstPage { get; set; }
        public System.Uri LastPage { get; set; }
        public System.Uri NextPage { get; set; }
        public System.Uri PreviousPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}