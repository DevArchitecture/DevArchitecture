using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
	/// <summary>
	/// Bu context birden fazla provider için migration takibi yapıldığı için
	/// varsayılan olarak Postg db'si üzerinden çalışır. Eğer sql geçmek isterseniz
	/// AddDbContext eklerken bundan türeyen MsDbContext'i kullanınız.
	/// </summary>
	public class NewDbContext : DbContext
	{
		protected readonly IConfiguration configuration;


		/// <summary>
		/// constructor da IConfiguration alıyoruz ki, birden fazla db ye parallel olarak
		/// migration yaratabiliyoruz.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="configuration"></param>
		public NewDbContext(DbContextOptions<NewDbContext> options, IConfiguration configuration)
										: base(options)
		{
			this.configuration = configuration;
		}

		/// <summary>
		/// Genel versiyonu da implement edelim.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="configuration"></param>
		protected NewDbContext(DbContextOptions options, IConfiguration configuration)
										: base(options)
		{
			this.configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				base.OnConfiguring(optionsBuilder.UseSqlServer(configuration.GetConnectionString("SFwMsContext")));
			}
		}
	}
}
