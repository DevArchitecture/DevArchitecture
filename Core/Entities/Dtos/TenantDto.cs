namespace Core.Entities.Dtos
{
    public class TenantDto : IDto
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public int? OrganizationId { get; set; }
    }
}
