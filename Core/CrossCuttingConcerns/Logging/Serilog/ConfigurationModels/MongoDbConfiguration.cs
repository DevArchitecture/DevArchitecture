namespace Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; }
        public string Collection { get; set; }
    }
}