using Core.DataAccess.MongoDb.Concrete.Configurations;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.MongoDb.Context
{
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
