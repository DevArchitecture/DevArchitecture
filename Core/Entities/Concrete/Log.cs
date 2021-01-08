using System;

namespace Core.Entities.Concrete
{
    public class Log : IEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Exception { get; set; }       
    }
}
