using Core.Entities;

namespace Entities.Dtos
{
    public class UpdateGroupDto : IDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
}