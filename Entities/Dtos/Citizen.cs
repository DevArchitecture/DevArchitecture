using Core.Entities;

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