namespace Core.Entities;

public abstract class CassDbEntity: IEntity
{
    public int Id { get; set; }
}