using System;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string TraceId { get; set; }
    }
}
