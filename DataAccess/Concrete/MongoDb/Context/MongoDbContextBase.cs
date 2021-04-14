namespace DataAccess.Concrete.MongoDb.Context
{
    using Core.DataAccess.MongoDb.Concrete.Configurations;
    using Microsoft.Extensions.Configuration;

    public abstract class MongoDbContextBase
    {
        public readonly IConfiguration Configuration;
        public readonly MongoConnectionSettings MongoConnectionSettings;

        protected MongoDbContextBase(IConfiguration configuration)
        {
            Configuration = configuration;
            MongoConnectionSettings = configuration.GetSection("MongoDbSettings").Get<MongoConnectionSettings>();
        }
    }
}
