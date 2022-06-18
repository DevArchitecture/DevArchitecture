namespace Core.Entities.Concrete;

public class Log : IEntity, ITenancy
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    public string MessageTemplate { get; set; }
    public string Level { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Exception { get; set; }
}
