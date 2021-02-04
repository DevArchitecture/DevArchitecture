using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
	public class Citizen : IDto
	{
		public long CitizenId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public int BirthYear { get; set; }
	}
}
