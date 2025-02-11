using System;

namespace Core.Utilities.Results
{
    [Serializable]
    public class ErrorResult : Result
    {
        public ErrorResult(string message)
            : base(false, message)
        {
        }

        public ErrorResult()
            : base(false)
        {
        }
    }
}