using Core.Entities;
using Core.Entities.Concrete;
//using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
	/// <summary>
	/// Bu context birden fazla provider için migration takibi yapıldığı için
	/// varsayılan olarak Postg db'si üzerinden çalışır. Eğer sql geçmek isterseniz
	/// AddDbContext eklerken bundan türeyen MsDbContext'i kullanınız.
	/// </summary>
	public class ProjectDbContext : DbContext
	{
		protected readonly IConfiguration configuration;


		/// <summary>
		/// constructor da IConfiguration alıyoruz ki, birden fazla db ye parallel olarak
		/// migration yaratabiliyoruz.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="configuration"></param>
		public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration)
										: base(options)
		{
			this.configuration = configuration;
		}

		/// <summary>
		/// Genel versiyonu da implement edelim.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="configuration"></param>
		protected ProjectDbContext(DbContextOptions options, IConfiguration configuration)
										: base(options)
		{
			this.configuration = configuration;
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				base.OnConfiguring(optionsBuilder.UseNpgsql(configuration.GetConnectionString("SFwPgContext")).EnableSensitiveDataLogging());

			}
		}

		public DbSet<OperationClaim> OperationClaims { get; set; }
		public DbSet<UserClaim> UserClaims { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<UserGroup> UserGroups { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<GroupClaim> GroupClaims { get; set; }
		public DbSet<Log> Logs { get; set; }
		public DbSet<MobileLogin> MobileLogins { get; set; }

	}
}
