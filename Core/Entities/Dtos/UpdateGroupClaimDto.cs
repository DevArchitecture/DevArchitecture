namespace Core.Entities.Dtos
{
    public class UpdateGroupClaimDto : IDto
    {
        public int GroupId { get; set; }
        public int[] ClaimIds { get; set; }
    }
}
