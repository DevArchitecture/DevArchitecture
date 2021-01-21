using MongoDB.Driver;

namespace Core.DataAccess.MongoDb.Concrete.Configurations
{
	public class MongoConnectionSettings
	{
		/// <summary>
		/// To be set if the MongoClientSetting class is to be used.
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
