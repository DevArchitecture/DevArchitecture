namespace Core.DataAccess.MongoDb.Concrete.Configurations
{
    using MongoDB.Driver;

    public class MongoConnectionSettings
	{
		public MongoConnectionSettings()
		{

		}

		public MongoConnectionSettings(MongoClientSettings mongoClientSettings)
		{
			MongoClientSettings = mongoClientSettings;
		}

		/// <summary>
		/// To be set if the MongoClientSetting class is to be used.
		/// </summary>
		private MongoClientSettings MongoClientSettings { get; set; }

		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }

		public MongoClientSettings GetMongoClientSettings()
		{
			return MongoClientSettings;
		}
	}
}
