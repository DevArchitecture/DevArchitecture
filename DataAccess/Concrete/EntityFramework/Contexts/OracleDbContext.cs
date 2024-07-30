using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public sealed class OracleDbContext(DbContextOptions<OracleDbContext> options, IConfiguration configuration) 
        : ProjectDbContext(options, configuration)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseOracle(Configuration.GetConnectionString("DArchOracleContext")));
            }
        }
    }
}