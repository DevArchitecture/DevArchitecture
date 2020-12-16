using Core.DataAccess.MongoDb.Concrete.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.MongoDb.Context
{
    public abstract class MongoDbContextBase
    {
        public readonly IConfiguration configuration;
        public readonly MongoConnectionSettings mongoConnectionSettings;

        protected MongoDbContextBase(IConfiguration configuration)
        {
            this.configuration = configuration;
            mongoConnectionSettings = configuration.GetSection("MongoDbSettings").Get<MongoConnectionSettings>();
        }
    }
}
