
using System;
using System.Collections.Generic;

namespace Core.Entities.Dtos
{
	public class LogDto : IDto
	{
		public int Id { get; set; }		
		public string Level { get; set; }
		public string ExceptionMessage { get; set; }
		public DateTimeOffset TimeStamp { get; set; }
		public string User { get; set; }
		public string Value { get; set; }
		public string Type { get; set; }
		
	}
	
   

   

}
