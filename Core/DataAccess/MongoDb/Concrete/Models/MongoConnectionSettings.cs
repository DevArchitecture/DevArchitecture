using MongoDB.Driver;

namespace Core.DataAccess.MongoDb.Concrete.Models
{
	public class MongoConnectionSettings
	{
		/// <summary>
		/// Eğer MongoClientSettingSınıfı kullanılacaksa set edilecek
		/// </summary>
		private MongoClientSettings _mongoClientSettings { get; set; }

		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
		public MongoConnectionSettings(MongoClientSettings mongoClientSettings)
		{
			_mongoClientSettings = mongoClientSettings;
		}
		public MongoConnectionSettings()
		{

		}

		public MongoClientSettings GetMongoClientSettings()
		{
			return _mongoClientSettings;
		}
	}
}
