using System.Collections.Generic;

namespace Core.Utilities.Results
{
    /// <summary>
    /// Paginated response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingResult<T>(List<T> data, int totalItemCount, 
        bool success, string message) : Result(success, message), IPagingResult<T>
    {
        public List<T> Data { get; } = data;
        public int TotalItemCount { get; } = totalItemCount;
    }
}
