namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}