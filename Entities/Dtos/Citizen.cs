namespace Entities.Dtos
{
    using Core.Entities;

    public class Citizen : IDto
	{
		public long CitizenId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public int BirthYear { get; set; }
	}
}
