using System.Collections.Generic;

namespace Core.Utilities.Results
{
    public interface IPagingResult<T> : IResult
    {
        /// <summary>
        /// data list
        /// </summary>
        List<T> Data { get; }

        /// <summary>
        /// total number of records
        /// </summary>
        int TotalItemCount { get; }
    }
}
