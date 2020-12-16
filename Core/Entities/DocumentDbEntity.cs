using MongoDB.Bson;

namespace Core.DataAccess.Concrete
{
	public abstract class DocumentDbEntity
	{
		public ObjectId Id { get; set; }
	}
}
