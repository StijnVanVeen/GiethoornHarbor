using HarborManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HarborManagementAPI.DataAccess
{
    public class HarborManagementDBContext : DbContext
    {
        public HarborManagementDBContext(DbContextOptions<HarborManagementDBContext> options) : base(options) { }

        public DbSet<Ship> Ships { get; set; }
        public DbSet<Dock> Docks { get; set; }
        public DbSet<Tugboat> Tugboat { get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ship>().HasKey(m => m.Id);
            builder.Entity<Dock>().HasKey(m => m.Id);
            builder.Entity<Tugboat>().HasKey(m => m.Id);

            builder.Entity<Ship>().HasOne<Dock>();
            builder.Entity<Tugboat>().HasOne<Ship>();

            builder.Entity<Ship>().ToTable("Ship");
            builder.Entity<Dock>().ToTable("Dock");
            builder.Entity<Tugboat>().ToTable("Tugboat");

            base.OnModelCreating(builder);
        }
    }
}
