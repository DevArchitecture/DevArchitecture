using Core.DataAccess.MongoDb.Concrete.Configurations;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.MongoDb.Context
{
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