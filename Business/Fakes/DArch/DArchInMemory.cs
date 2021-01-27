using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Fakes.DArch
{
	public sealed class DArchInMemory : ProjectDbContext
	{
		public DArchInMemory(DbContextOptions<DArchInMemory> options, IConfiguration configuration) : base(options, configuration)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				base.OnConfiguring(optionsBuilder.UseInMemoryDatabase(configuration.GetConnectionString("DArchInMemory")));
								
			}
		}
		
	}
}
