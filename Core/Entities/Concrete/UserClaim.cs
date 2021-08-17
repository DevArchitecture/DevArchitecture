namespace Core.Entities.Concrete
{
    public class UserClaim : IEntity
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }
    }
}