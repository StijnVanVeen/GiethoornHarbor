using Microsoft.EntityFrameworkCore;
using Polly;
using ShipManagementAPI.Model;

namespace ShipManagementAPI.DataAccess;

public class ShipManagementDBContext : DbContext
{
    public ShipManagementDBContext(DbContextOptions<ShipManagementDBContext> options) : base(options)
    {
    }

    public DbSet<Ship> Ships { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Ship>().HasKey(m => m.ID);
        builder.Entity<Ship>().ToTable("Ship");
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