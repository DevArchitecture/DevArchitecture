namespace DataAccess.Concrete.MongoDb.Context
{
    using Core.DataAccess.MongoDb.Concrete.Configurations;
    using Microsoft.Extensions.Configuration;

    public abstract class MongoDbContextBase
    {
        protected MongoDbContextBase(IConfiguration configuration)
        {
            Configuration = configuration;
            MongoConnectionSettings = configuration.GetSection("MongoDbSettings").Get<MongoConnectionSettings>();
        }

        public IConfiguration Configuration { get; }
        public MongoConnectionSettings MongoConnectionSettings { get; }
    }
}
