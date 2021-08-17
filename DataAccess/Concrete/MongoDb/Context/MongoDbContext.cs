using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.MongoDb.Context
{
    public class MongoDbContext : MongoDbContextBase
    {
        public MongoDbContext(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}