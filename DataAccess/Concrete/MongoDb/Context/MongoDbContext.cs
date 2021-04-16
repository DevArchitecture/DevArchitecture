namespace DataAccess.Concrete.MongoDb.Context
{
    using Microsoft.Extensions.Configuration;

    public class MongoDbContext : MongoDbContextBase
    {
        public MongoDbContext(IConfiguration configuration)
            : base(configuration)
        {

        }

    }
}
