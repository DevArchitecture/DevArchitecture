using Core.Entities;

namespace Entities.Dtos
{
    public class UpdateGroupDto : IDto
    {
        public string GroupName { get; set; }
    }
}