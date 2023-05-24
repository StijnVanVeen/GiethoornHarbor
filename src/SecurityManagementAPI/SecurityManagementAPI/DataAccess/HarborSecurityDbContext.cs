using Microsoft.EntityFrameworkCore;
using SecurityManagementAPI.Models;

namespace SecurityManagementAPI.DataAccess
{
	public class HarborSecurityDbContext : DbContext
	{
		public HarborSecurityDbContext(DbContextOptions<HarborSecurityDbContext> options) : base(options) { }

		public DbSet<Ship> Ships { get; set; }
		public DbSet<Truck> Trucks { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Ship>().HasKey(m => m.Id);
			builder.Entity<Truck>().HasKey(m => m.Id);

			builder.Entity<Truck>().HasOne<Ship>();

			base.OnModelCreating(builder);
		}
	}
}
