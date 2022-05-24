using Core.Entities;

namespace Entities.Dtos
{
    public class UpdateGroupClaimDto : IDto
    {
        public int GroupId { get; set; }
        public int[] ClaimIds { get; set; }
    }
}