namespace Core.Entities.Dtos
{
    public class UpdateOperationClaimDto : IDto
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
    }
}
