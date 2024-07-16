using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public sealed class MySqlDbContext(DbContextOptions<MySqlDbContext> options, IConfiguration configuration) 
        : ProjectDbContext(options, configuration)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseMySQL(Configuration.GetConnectionString("DArchMySqlContext")));
            }
        }
    }
}