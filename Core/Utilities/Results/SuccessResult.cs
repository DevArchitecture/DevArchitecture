using System;
using System.Text.Json.Serialization;

namespace Core.Utilities.Results
{
    [Serializable]
    public class SuccessResult : Result
    {
        public SuccessResult(string message)
            : base(true, message)
        {
        }

        public SuccessResult()
            : base(true)
        {
        }
    }
}