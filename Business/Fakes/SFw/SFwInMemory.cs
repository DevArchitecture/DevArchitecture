using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Fakes.SFw
{
	public sealed class SFwInMemory : ProjectDbContext
	{
		public SFwInMemory(DbContextOptions<SFwInMemory> options, IConfiguration configuration) : base(options, configuration)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				base.OnConfiguring(optionsBuilder.UseInMemoryDatabase(configuration.GetConnectionString("SFwInmemory")));
								
			}
		}
		
	}
}
