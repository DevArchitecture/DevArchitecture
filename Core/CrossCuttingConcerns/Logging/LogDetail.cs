using System.Collections.Generic;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string FullName { get; set; }
        public string MethodName { get; set; }
        public string User { get; set; }
        public List<LogParameter> Parameters { get; set; }
    }
}