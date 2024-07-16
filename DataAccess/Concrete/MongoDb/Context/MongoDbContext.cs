using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.MongoDb.Context
{
    public class MongoDbContext(IConfiguration configuration) : MongoDbContextBase(configuration)
    {
    }
}