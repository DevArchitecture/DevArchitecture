namespace Core.Entities
{
    using MongoDB.Bson;

    public abstract class DocumentDbEntity
	{
		public ObjectId Id { get; set; }
		public string ObjectId => Id.ToString();
	}
}
