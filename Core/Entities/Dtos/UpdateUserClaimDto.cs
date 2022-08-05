namespace Core.Entities.Dtos
{
    public class UpdateUserClaimDto : IDto
    {
        public int UserId { get; set; }
        public int[] ClaimIds { get; set; }
    }
}
