namespace DataAccess.Concrete.EntityFramework.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public sealed class MySqlDbContext : ProjectDbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options, IConfiguration configuration)
            : base(options, configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseMySQL(Configuration.GetConnectionString("DArchMySqlContext")));
            }
        }
    }
}
