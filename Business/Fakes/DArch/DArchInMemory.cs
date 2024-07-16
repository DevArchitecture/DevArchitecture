using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Business.Fakes.DArch
{
    public sealed class DArchInMemory(DbContextOptions<DArchInMemory> options,
        IConfiguration configuration) : ProjectDbContext(options, configuration)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(
                    optionsBuilder.UseInMemoryDatabase(Configuration.GetConnectionString("DArchInMemory")));
            }
        }
    }
}