﻿using System;

namespace Core.Utilities.Results
{
    [Serializable]
    public class Result : IResult
    {
        public Result(bool success, string message)
            : this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}