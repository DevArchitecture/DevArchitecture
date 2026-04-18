using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Storage;

namespace Business.Fakes.DArch
{
    public sealed class DArchInMemory : ProjectDbContext
    {
        private static readonly InMemoryDatabaseRoot SharedDatabaseRoot = new();

        public DArchInMemory(DbContextOptions<DArchInMemory> options, IConfiguration configuration)
            : base(options, configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(
                    optionsBuilder.UseInMemoryDatabase(
                        Configuration.GetConnectionString("DArchInMemory"),
                        SharedDatabaseRoot));
            }
        }
    }
}