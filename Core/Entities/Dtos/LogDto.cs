using System;

namespace Core.Entities.Dtos
{
    public class LogDto : IEntity
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string ExceptionMessage { get; set; }
        public DateTime TimeStamp { get; set; }
        public string User { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}