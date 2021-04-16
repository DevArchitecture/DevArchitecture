namespace Core.Entities.Concrete
{
    using System;

    public class Log : IEntity
	{
		public int Id { get; set; }
		public string MessageTemplate { get; set; }
		public string Level { get; set; }
		public DateTimeOffset TimeStamp { get; set; }
		public string Exception { get; set; }
	}
}
