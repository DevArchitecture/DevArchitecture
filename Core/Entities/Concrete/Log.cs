using System;

namespace Core.Entities.Concrete
{
    public class Log : IEntity
    {
        public int Id { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
    }
}