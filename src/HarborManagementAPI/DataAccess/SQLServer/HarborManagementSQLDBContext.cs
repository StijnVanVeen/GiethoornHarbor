using HarborManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HarborManagementAPI.DataAccess
{
    public class HarborManagementSQLDBContext : DbContext
    {
        public HarborManagementSQLDBContext(DbContextOptions<HarborManagementSQLDBContext> options) : base(options) { }

        public DbSet<Ship> Ships { get; set; }
        public DbSet<Dock> Docks { get; set; }
        public DbSet<Tugboat> Tugboat { get; set;}
        public DbSet<Arrival> Arrivals { get; set; }
        public DbSet<Departure> Departures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ship>().HasKey(m => m.Id);
            builder.Entity<Dock>().HasKey(m => m.Id);
            builder.Entity<Tugboat>().HasKey(m => m.Id);
            builder.Entity<Arrival>().HasKey(m => m.Id);
            builder.Entity<Departure>().HasKey(m => m.Id);

            builder.Entity<Ship>().ToTable("Ship");
            builder.Entity<Dock>().ToTable("Dock");
            builder.Entity<Tugboat>().ToTable("Tugboat");
            builder.Entity<Arrival>().ToTable("Arrival");
            builder.Entity<Departure>().ToTable("Departure");

            base.OnModelCreating(builder);
        }
        
        public void MigrateDB()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.Migrate());
        }
    }
}
