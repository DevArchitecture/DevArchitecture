namespace Core.Entities.Concrete;

public class Group : BaseEntity, ITenancy
{
    public int TenantId { get; set; }
    public string GroupName { get; set; }

}
